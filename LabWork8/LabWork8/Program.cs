namespace LabWork8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = new MyNamespace.MyClass().GetPassword();

            Console.Write("Введите имя пользователя: ");
            string username = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string userPassword = Console.ReadLine();

            if (userPassword == password)
                Console.WriteLine("ACCESS_GRANTED");
            else
                Console.WriteLine("ACCESS_DENIED");
        }
    }
}
