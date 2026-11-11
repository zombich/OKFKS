


await GetDataFromApi("/success");
await GetDataFromApi("/temporary");
await GetDataFromApi("/timeout");
await GetDataFromApi("/unavailable");
await GetDataFromApi("/bad-request");

async Task GetDataFromApi(string endpoint)
{
    Console.WriteLine($"Получаем данные с {endpoint}");
    int maxAttempts = 4;
    double[] delays = [0.5, 1, 2];
    bool isEnd = false;
    bool isSuccess = false;

    using var client = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:8080")
    };
    await client.GetAsync("/reset");

    for (int i = 0; i <= maxAttempts - 1; i++)
    {
        string response;
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

        try
        {
            var result = await client.GetAsync(endpoint, cts.Token);


            if (result.IsSuccessStatusCode)
            {
                isEnd = true;
                isSuccess = true;
            }
            else if ((int)result.StatusCode == 400 || (int)result.StatusCode == 401 || (int)result.StatusCode == 403 || (int)result.StatusCode == 404)
            {
                isEnd = true;
            }
            response = result.StatusCode.ToString();
        }
        catch (Exception ex)
        {
            response = ex.Message;
        }


        if (i < maxAttempts - 1)
        {
            Console.WriteLine($"Попытка {i + 1} - Результат {response} - Следующая задержка - {delays[i]}");
            await Task.Delay(TimeSpan.FromSeconds(delays[i]));
        }
        else
        {
            Console.WriteLine($"Попытка {i + 1} - Результат {response}");
            if (isSuccess)
                Console.WriteLine($"Всего попыток - {i + 1} Итого - выполнено успешно");
            else
                Console.WriteLine($"Всего попыток - {i + 1} Итого - выполнено неуспешно");
        }


        if (isEnd)
            return;

    }
}

