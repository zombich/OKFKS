namespace LabWork7
{
    public interface IActivity
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Date { get; set; }
        string Description { get; set; }

    }

    public class MyActivity : IActivity
    {
        private static List<MyActivity> Activities = new();
        private static int LastId { get; set; } = 1;
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public static void CreateActivity(string name, DateTime date, string description)
        {
            var activity = new MyActivity()
            {
                Id = LastId,
                Name = name,
                Date = date,
                Description = description
            };
            LastId++;

            Activities.Add(activity);
        }

        public static List<MyActivity> GetAcitivities() => Activities;

        public static MyActivity? GetActivityById(int id) => Activities.Where(a => a.Id == id).FirstOrDefault();

        public static void UpdateActivity(int id, string name, DateTime date, string description)
        {
            var selectedActivity = Activities.Where(a => a.Id == id).FirstOrDefault();

            if (selectedActivity is null)
                return;

            Activities.Remove(selectedActivity);

            selectedActivity.Name = name;
            selectedActivity.Date = date;
            selectedActivity.Description = description;

            Activities.Add(selectedActivity);

        }

        public static void RemoveActivity(int id)
        {
            var selectedActivity = Activities.Where(a => a.Id == id).FirstOrDefault();
            if (selectedActivity is not null)
                Activities.Remove(selectedActivity);
        }
    }




}
