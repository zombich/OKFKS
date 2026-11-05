//var pipe = new System.IO.Pipes.NamedPipeClientStream(".", "mypipe", System.IO.Pipes.PipeDirection.Out);
//pipe.Connect();
//var writer = new StreamWriter(pipe);
//writer.WriteLine("Message(\"Hello\")");
//writer.Flush();
//writer.Close();
//pipe.Close();


using LabWork7;

DateTime date1 = DateTime.Parse("12-10-2026 23:32:59");
DateTime date2 = DateTime.Parse("07-10-2021 20:10:00");
DateTime date3 = DateTime.Parse("05-09-2023 13:56:00");
DateTime date4 = DateTime.Parse("10-11-2025 15:22:00");
DateTime date5 = DateTime.Parse("10-11-2026 10:32:00");

Event.CreateActivity("activity1", date1, "best activity");
Event.CreateActivity("fish", date2, "fishing");
Event.CreateActivity("activity33", date3, "z");
Event.CreateActivity("act4", date4, "top");
Event.CreateActivity("activ6", date5, "12312321 qweqwe");

var act = Event.GetActivityById(1);
Console.WriteLine(act.Name);

Event.UpdateActivity(1, "upd activ", date2, "upd desc");
act = Event.GetActivityById(1);

Console.WriteLine(act.Name);

Event.RemoveActivity(2);

var activities = Event.GetAcitivities();

act = activities.FirstOrDefault();

Console.WriteLine(act.Description);