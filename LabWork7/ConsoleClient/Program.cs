var pipe = new System.IO.Pipes.NamedPipeClientStream(".", "mypipe", System.IO.Pipes.PipeDirection.Out);

pipe.Connect();

var writer = new StreamWriter(pipe);
writer.WriteLine("Message(\"Hello\")");

writer.Flush();
writer.Close();
pipe.Close();