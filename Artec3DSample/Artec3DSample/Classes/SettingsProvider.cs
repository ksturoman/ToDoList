using Artec3DSample.Interfaces;
using Newtonsoft.Json;
using Plugin.Settings;
using System;

namespace Artec3DSample.Classes
{
    public class SettingsProvider : ISettingsProvider
    {
        public static string Tasks = "Tasks";

        public event AddOrUpdateFiredDelegate AddOrUpdateFired;

        public T GetJsonValueOrDefault<T>(string key, T defaultValue = default, string fileName = null)
        {
            T result;

            try
            {
                var jsonValue = GetValueOrDefault(key, "");

                result = JsonConvert.DeserializeObject<T>(jsonValue);
            }
            catch
            {
                result = defaultValue;
            }

            if (result == null)
            {
                result = defaultValue;
            }

            return result;
        }

        public decimal GetValueOrDefault(string key, decimal defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public bool GetValueOrDefault(string key, bool defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public long GetValueOrDefault(string key, long defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public string GetValueOrDefault(string key, string defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public int GetValueOrDefault(string key, int defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public float GetValueOrDefault(string key, float defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public DateTime GetValueOrDefault(string key, DateTime defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public Guid GetValueOrDefault(string key, Guid defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public double GetValueOrDefault(string key, double defaultValue, string fileName = null)
        {
            return CrossSettings.Current.GetValueOrDefault(key, defaultValue, fileName);
        }

        public bool AddOrUpdateJsonValue<T>(string key, T value, string fileName = null)
        {
            var jsonValue = JsonConvert.SerializeObject(value);

            var result = CrossSettings.Current.AddOrUpdateValue(key, jsonValue, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, decimal value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, bool value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, long value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, string value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, int value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, float value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, DateTime value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, Guid value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public bool AddOrUpdateValue(string key, double value, string fileName = null)
        {
            var result = CrossSettings.Current.AddOrUpdateValue(key, value, fileName);

            AddOrUpdateFired?.Invoke(key);

            return result;
        }

        public void Remove(string key, string fileName = null)
        {
            CrossSettings.Current.Remove(key, fileName);
        }

        public void Clear(string fileName = null)
        {
            CrossSettings.Current.Clear(fileName);
        }

        public bool Contains(string key, string fileName = null)
        {
            return CrossSettings.Current.Contains(key, fileName);
        }

        public bool OpenAppSettings()
        {
            return CrossSettings.Current.OpenAppSettings();
        }
    }
}
