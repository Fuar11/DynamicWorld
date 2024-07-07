namespace DynamicWorld
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        public enum Active
        {
            Disabled, Enabled
        }

        [Section("Transition Zones")]

        [Name("Hard Lock")]
        [Choice("Disabled", "Enabled")]
        public Active hardLock = Active.Disabled;

        // this is called whenever there is a change. Ensure it contains the null bits that the base method has
        protected override void OnChange(FieldInfo field, object? oldValue, object? newValue)
        {
            base.OnChange(field, oldValue, newValue);
        }

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}