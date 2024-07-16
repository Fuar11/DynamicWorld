using DynamicWorld.Earthquake;
using DynamicWorld.Environment;
using DynamicWorld.Utilities.JSON;
using MelonLoader.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DynamicWorld.Utilities
{
    internal class SaveDataManager
    {

        ModDataManager dm = new ModDataManager("Dynamic World", true);

        /**
        public async Task Save(EarthquakeSaveDataProxy data)
        {
            await JsonFile.SaveAsync<EarthquakeSaveDataProxy>($"{MelonEnvironment.ModsDirectory}/DynamicWorld.json", data);
        }

        public async Task<EarthquakeSaveDataProxy?> Load()
        {
            return await JsonFile.LoadAsync<EarthquakeSaveDataProxy>($"{MelonEnvironment.ModsDirectory}/DynamicWorld.json", true);
        } **/

        public void Save(EarthquakeSaveDataProxy data)
        {
            string? dataString;
            dataString = JsonSerializer.Serialize<EarthquakeSaveDataProxy>(data);
            dm.Save(dataString, "EarthquakeData");
        }

        public void Save(TransitionZoneDataSaveDataProxy data)
        {
            string? dataString;
            dataString = JsonSerializer.Serialize<TransitionZoneDataSaveDataProxy>(data);
            dm.Save(dataString, "TransitionZoneData");
        }

        public EarthquakeSaveDataProxy Load()
        {
            string? dataString = dm.Load("EarthquakeData");
            if (dataString is null) 
            {
                Main.Logger.Log("Loaded Earthquake Data is null...", ComplexLogger.FlaggedLoggingLevel.Debug);
                return null;
            }

            EarthquakeSaveDataProxy? data = JsonSerializer.Deserialize<EarthquakeSaveDataProxy>(dataString);
            return data;
        }

        public TransitionZoneDataSaveDataProxy LoadTz()
        {

            string? dataString = dm.Load("TransitionZoneData");
            if (dataString is null) return null;

            TransitionZoneDataSaveDataProxy? data = JsonSerializer.Deserialize<TransitionZoneDataSaveDataProxy>(dataString);
            return data;
        }
    }
}
