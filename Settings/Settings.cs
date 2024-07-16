namespace DynamicWorld
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        public enum Active
        {
            Disabled, Enabled
        }

        [Section("Earthquakes")]

        [Name("Minimum Time to Earthquake")]
        [Description("The minimum time it takes for the next earthquake to occur in days.")]
        [Slider(15, 60)]
        public int eqMinTime = 15;

        [Name("Maximum Time to Earthquake")]
        [Description("The maximum time it takes for the next earthquake to occur in days.")]
        [Slider(60, 90)]
        public int eqMaxTime = 60;

        [Section("Transition Zones")]

        [Name("Dead End")]
        [Description("With this enabled, certain regions with a single transition zone or a set of transition zones can get blocked, rendering the entire region inaccessible or trapping you there.")]
        [Choice("Disabled", "Enabled")]
        public Active hardLock = Active.Disabled;

        [Name("Roll Transition Zones on Start")]
        [Description("Transition Zones will be rolled when starting a new game.")]
        [Choice("Disabled", "Enabled")]
        public Active transitionZoneRollStart = Active.Disabled;

        // this is called whenever there is a change. Ensure it contains the null bits that the base method has
        protected override void OnChange(FieldInfo field, object? oldValue, object? newValue)
        {
            if (field.Name == nameof(eqMinTime)) RefreshValues("fpMaxTime");
            else if (field.Name == nameof(eqMaxTime)) RefreshValues("fpMinTime");

            RefreshGUI();
        }

        internal void RefreshValues(string fieldName)
        {

            if (fieldName == "fpMinTime")
            {
                if (eqMaxTime < eqMinTime) eqMinTime = eqMaxTime - 1;
                eqMinTime = Mathf.Clamp(eqMinTime, 15, 60);
            }
            else if (fieldName == "fpMaxTime")
            {
                if (eqMinTime > eqMaxTime) eqMaxTime = eqMinTime + 1;
                eqMaxTime = Mathf.Clamp(eqMaxTime, 15, 60);
            }
        }

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}