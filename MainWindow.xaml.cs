using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Notifications.Models;
using Notifications.Utilities;

namespace Notifications
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _mainTimer;
        private static string _cachePath = "c:\\temp\\.n_it_2020_c";
        private string _cs = string.Empty;
        private List<RunningTimers> _runningTimers;
        private Logs _logger;
        private string _machineName;
        private int _errorCount = 0;
        private bool _firstRun = true;

        public MainWindow()
        {
            Hide();
            InitializeComponent();
            _machineName = Environment.MachineName;
            if (GetConfig())
            {
                _mainTimer = new DispatcherTimer();
                _mainTimer.Interval = TimeSpan.FromSeconds(5);
                _mainTimer.Tick += _mainTimer_Tick;
                _runningTimers = new List<RunningTimers>();
                _mainTimer.Start();
            }
            else
                Close();

        }

        private void _mainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _mainTimer.IsEnabled = false;
                var notifications = GetNotifications();

                if (notifications != null)
                {

                    var runningToKill = _runningTimers.Where(x => !notifications.Select(y => y.Id).ToArray().Contains(x.NotificationId)).ToArray();
                    if (runningToKill.Any())
                    {
                        foreach (var t in runningToKill)
                        {
                            t.Timer.Stop();
                            _runningTimers.Remove(t);
                        }
                    }

                    var executed = File.ReadAllText(_cachePath);
                    foreach (var notification in notifications)
                    {
                        if (executed.Length > 0 && executed.Split(';').Contains(notification.Id.ToString()) && !_firstRun)
                        {
                            if (notification.ValidTo < DateTime.Now)
                            {
                                _runningTimers.FirstOrDefault(x => x.NotificationId == notification.Id)?.Timer.Stop();
                            }
                        }
                        else
                        {
                            var notificationParams = GetParameters(notification.Id);
                            if (notificationParams == null)
                                continue;

                            if (notification.Repeat > 0)
                            {
                                var taskRunner = new DispatcherTimer();
                                taskRunner.Interval = TimeSpan.FromMinutes(notification.Repeat);
#if DEBUG
                                taskRunner.Interval = TimeSpan.FromSeconds(20);
#endif
                                taskRunner.Tick += (o, args) => { new Info().LoadParameters(notificationParams); };
                                taskRunner.Start();
                                _runningTimers.Add(new RunningTimers()
                                {
                                    Timer = taskRunner,
                                    NotificationId = notification.Id
                                });

                            }
                            if (!executed.Split(';').Contains(notification.Id.ToString()))
                            {
                                using (var app = File.AppendText(_cachePath))
                                {
                                    app.Write($"{notification.Id};");
                                }
                            }

                            new Info().LoadParameters(notificationParams);
                        }
                    }
                }
                else
                {
                    foreach (var runningTimer in _runningTimers)
                    {
                        runningTimer.Timer.Stop();
                    }
                    _runningTimers.Clear();
                }
                _firstRun = false;
                _mainTimer.IsEnabled = true;
            }
            catch(Exception ex)
            {
                _logger.LogEvent(_machineName, $"App closed due to {ex.Message}", EventLogEntryType.Error);
                _mainTimer.Stop();
                foreach (var t in _runningTimers)
                {
                    t.Timer.Stop();
                }
                Close();
            }

        }

        private bool GetConfig()
        {
            try
            {
                if (Process.GetProcessesByName("Notifications").Length > 1)
                    return false;

                if (File.Exists("c:\\.conns"))
                {
                    _cs = File.ReadAllText("c:\\.conns");
                    _logger = new Logs(_cs);
                }
                else
                {
                    _logger.LogEvent(_machineName, $"Connection string file doesnt exist", EventLogEntryType.Error, true);
                    return false;
                }
                    

                if (_cs.Length == 0)
                {
                    _logger.LogEvent(_machineName, $"Connection string file is empty", EventLogEntryType.Error, true);
                    return false;
                }
                    

                var tmp = _cs.Split('/').Select(x => int.Parse(x) + 5).Select(x => byte.Parse($"{x}")).ToArray();
                _cs = Encoding.ASCII.GetString(tmp);

                if (!File.Exists(_cachePath))
                {
                    using (var file = File.CreateText(_cachePath)) { }
                }

                if (!DatabaseHandler.CheckConnection(_cs))
                {
                    _logger.LogEvent(_machineName, "Cannot connect to the database", EventLogEntryType.Error, true);
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogEvent(_machineName, $"Cannot get configuration due to {e.Message}", EventLogEntryType.Error);
                return false;
            }

        }

        private IEnumerable<Models.Notificarion> GetNotifications()
        {
            try
            {
                var db = new DatabaseHandler(_cs);
                var data = db.GetData("sp_GetActiveNotifications");
                if (data.Rows.Count > 0)
                {
                    return data.AsEnumerable().Select(x => new Notificarion()
                    {
                        Id = x.Field<int>("Id"),
                        Name = x.Field<string>("Name"),
                        ValidFrom = x.Field<DateTime>("ValidFrom"),
                        ValidTo = x.Field<DateTime>("ValidTo"),
                        Repeat = x.Field<int>("Repeat")
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogEvent(_machineName, $"Cannot get notifications due to {e.Message}", EventLogEntryType.Warning);
                _errorCount++;
                return null;
            }
        }

        private Parameters GetParameters(int notificationId)
        {
            try
            {
                var db = new DatabaseHandler(_cs);
                var data = db.GetData("sp_GetNotificationParameters", null, new[] { new SqlParameter("@NotificationId", notificationId) });
                if (data.Rows.Count > 0)
                {
                    return data.AsEnumerable().Select(x => new Parameters()
                    {
                        Title = x.Field<string>("Title"),
                        Autosize = x.Field<bool>("Autosize"),
                        BgColor = x.Field<string>("BackgroundColor"),
                        BgImage = x.Field<string>("BackgroundImage"),
                        Height = x.Field<int>("Height"),
                        NotificationText = x.Field<string>("NotificationText"),
                        TextColor = x.Field<string>("TextColor"),
                        Maximized = x.Field<bool>("Maximized"),
                        Width = x.Field<int>("Width"),
                        Gradient = x.Field<string>("Gradient")
                    }).First();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogEvent(_machineName, $"Cannot get parameters due to {e.Message}", EventLogEntryType.Warning);
                _errorCount++;
                return null;
            }
        }
    }
}
