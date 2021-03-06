﻿using PhotoCollageScreensaver.Contracts;
using System.IO;
using System.Text.Json;

namespace PhotoCollageScreensaver.Repositories
{
    internal class FileSystemConfigurationRepository : IConfigurationRepository
    {
        private readonly string directoryPath;
        private readonly string filePath;

        public FileSystemConfigurationRepository(string configurationFolderPath)
        {
            this.directoryPath = configurationFolderPath;
            this.filePath = Path.Combine(this.directoryPath, @"photo-collage.config");
            this.EnsureDirectoryExists();
            this.EnsureFileExists();
        }

        public Configuration Load()
        {
            string contents = File.ReadAllText(this.filePath);
            return contents.Trim().StartsWith("<?xml")
                ? this.LoadFromXml(contents) // provides fallback for upgrades from older version
                : this.LoadFromJson(contents);
        }

        public void Save(Configuration configuration)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var json = JsonSerializer.Serialize(configuration, options);
            File.WriteAllText(this.filePath, json);
        }

        private Configuration LoadFromJson(string contents)
        {
            return JsonSerializer.Deserialize<Configuration>(contents);
        }

        private Configuration LoadFromXml(string contents)
        {
            contents = contents.Replace("ScreensaverConfiguration", "Configuration");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
            using (TextReader reader = new StringReader(contents))
            {
                return serializer.Deserialize(reader) as Configuration;
            }
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(this.directoryPath))
            {
                Directory.CreateDirectory(this.directoryPath);
            }
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(this.filePath))
            {
                File.CreateText(this.filePath).Close();
                this.Save(new Configuration());
            }
        }
    }
}
