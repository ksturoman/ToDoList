using Plugin.Settings.Abstractions;

namespace Artec3DSample.Interfaces
{
    public delegate void AddOrUpdateFiredDelegate(string key);

    public interface ISettingsProvider : ISettings
    {
        event AddOrUpdateFiredDelegate AddOrUpdateFired;

        T GetJsonValueOrDefault<T>(string key, T defaultValue = default, string fileName = null);

        bool AddOrUpdateJsonValue<T>(string key, T value, string fileName = null);
    }
}
