using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Utilities
{
    class OfflineMode
    {
        private string _fileLocation = "c:\\temp\\.n_it_2020_ns";
        private string _photoDefaultLocation = "c:\\temp\\.n_it_2020_p_";
        public void SaveLocally(Models.Notification[] notificarions)
        {
            try
            {
                foreach (var item in notificarions)
                {
                    try
                    {
                        if(item.Parameters.BgImage.Length > 0)
                        {
                            var newFileName = $"{_photoDefaultLocation}{DateTime.Now:yyyyMMddHHmmssfff}.{item.Parameters.BgImage.Split('.').Last()}";
                            File.Copy(item.Parameters.BgImage, newFileName);
                            item.Parameters.BgImage = newFileName;
                        }
                    }
                    catch { }

                    try
                    {
                        foreach (var file in Directory.GetFiles("c:\\temp\\", ".n_it_*_p_*", SearchOption.TopDirectoryOnly))
                        {
                            var tmp = new FileInfo(file);
                            if (tmp.CreationTime.AddDays(14) < DateTime.Now)
                            {
                                File.Delete(file);
                            }
                        }
                    }
                    catch { }
                }

                WriteToBinaryFile(notificarions);
            }
            catch (Exception e)
            {

                    throw;
            }
        }

        public Models.Notification[] GetNotifications()
        {
            try
            {
                return ReadFromBinaryFile<Models.Notification[]>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void WriteToBinaryFile<T>(T objectToWrite)
        {
            using (Stream stream = File.Open(_fileLocation, FileMode.Create ))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private T ReadFromBinaryFile<T>()
        {
            using (Stream stream = File.Open(_fileLocation, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
