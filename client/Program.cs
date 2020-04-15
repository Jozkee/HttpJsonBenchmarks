using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HttpJsonClient
{
    class Program
    {
        private static HttpClient Http = new HttpClient();

        static async Task Main(string[] args)
        {
            if (args.Length < 6)
            {
                Console.WriteLine("Warmup<int> Duration<int> NewJson<bool> Method<string> Url<string> arguments required");
                return;
            }

            if (!int.TryParse(args[0], out int warmup))
            {
                Console.WriteLine($"Invalid arg 'warmup': {args[0]}");
                return;
            }

            if (!int.TryParse(args[1], out int duration))
            {
                Console.WriteLine($"Invalid arg 'duration': {args[1]}");
                return;
            }

            if (!bool.TryParse(args[2], out bool newJson))
            {
                Console.WriteLine($"Invalid arg 'newJson': {args[2]}");
                return;
            }

            if (!Enum.TryParse(args[3], ignoreCase: true, out TestMethod method)) 
            {
                Console.WriteLine($"Invalid arg 'method': {args[3]}");
                return;
            }

            if (!Enum.TryParse(args[4], ignoreCase: true, out ClassType classType)) 
            {
                Console.WriteLine($"Invalid arg 'method': {args[4]}");
                return;
            }

            string url = args[5];

            Console.WriteLine($"Running {method}{classType} benchmark...");

            switch (method)
            {
                case TestMethod.Get:
                    switch(classType)
                    {
                        case ClassType.Collection:
                            await RunGetJsonCollectionMethod(warmup, newJson, url, isWarmup: true);
                            await RunGetJsonCollectionMethod(duration, newJson, url, isWarmup: false);
                            break;

                        case ClassType.Object:
                            await RunGetJsonObjectMethod(warmup, newJson, url, isWarmup: true);
                            await RunGetJsonObjectMethod(duration, newJson, url, isWarmup: false);
                            break;
                        default:
                            throw new Exception("Invalid class type.");
                    }
                    break;

                case TestMethod.Post:
                    switch(classType)
                    {
                        case ClassType.Collection:
                            await RunPostJsonCollectionMethod(warmup, newJson, url, isWarmup: true);
                            await RunPostJsonCollectionMethod(duration, newJson, url, isWarmup: false);
                            break;

                        case ClassType.Object:
                            await RunPostJsonObjectMethod(warmup, newJson, url, isWarmup: true);
                            await RunPostJsonObjectMethod(duration, newJson, url, isWarmup: false);
                            break;
                        default:
                            throw new Exception("Invalid class type.");
                    }
                    break;

                default:
                    throw new Exception("Invalid method.");
            }
        }

        private static async Task RunGetJsonCollectionMethod(int duration, bool newJson, string url, bool isWarmup)
        {
            if (isWarmup)
            {
                Console.WriteLine("Warmup...");
            }
            else
            {
                Console.WriteLine("Measuring...");
            }

            var running = true;
            var iterations = 0;

            var job = Task.Run(async () =>
            {
                while (running)
                {
                    if (newJson)
                    {
                        await Http.GetFromJsonAsync<List<WeatherForecast>>(url);
                    }
                    else
                    {
                        await Http.GetJsonAsync<List<WeatherForecast>>(url);
                    }

                    iterations++;
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(duration));
            running = false;

            await job;

            if (!isWarmup)
            {
                Console.WriteLine($"{iterations} iterations in {duration}s");
            }
        }

        private static async Task RunGetJsonObjectMethod(int duration, bool newJson, string url, bool isWarmup)
        {
            if (isWarmup)
            {
                Console.WriteLine("Warmup...");
            }
            else
            {
                Console.WriteLine("Measuring...");
            }

            var running = true;
            var iterations = 0;

            var job = Task.Run(async () =>
            {
                while (running)
                {
                    if (newJson)
                    {
                        await Http.GetFromJsonAsync<WeatherForecast>(url);
                    }
                    else
                    {
                        await Http.GetJsonAsync<WeatherForecast>(url);
                    }

                    iterations++;
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(duration));
            running = false;

            await job;

            if (!isWarmup)
            {
                Console.WriteLine($"{iterations} iterations in {duration}s");
            }
        }

        private static async Task RunPostJsonCollectionMethod(int duration, bool newJson, string url, bool isWarmup)
        {
            if (isWarmup)
            {
                Console.WriteLine("Warmup...");
            }
            else
            {
                Console.WriteLine("Measuring...");
            }

            var running = true;
            var iterations = 0;

            List<WeatherForecast> value = WeatherForecast.GetWeatherForecast();

            var job = Task.Run(async () =>
            {
                while (running)
                {
                    if (newJson)
                    {
                        await Http.PostAsJsonAsync(url, value);
                    }
                    else
                    {
                        await Http.PostJsonAsync(url, value);
                    }

                    iterations++;
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(duration));
            running = false;

            await job;

            if (!isWarmup)
            {
                Console.WriteLine($"{iterations} iterations in {duration}s");
            }
        }

        private static async Task RunPostJsonObjectMethod(int duration, bool newJson, string url, bool isWarmup)
        {
            if (isWarmup)
            {
                Console.WriteLine("Warmup...");
            }
            else
            {
                Console.WriteLine("Measuring...");
            }

            var running = true;
            var iterations = 0;

            WeatherForecast value = WeatherForecast.GetSingleWeatherForecast();

            var job = Task.Run(async () =>
            {
                while (running)
                {
                    if (newJson)
                    {
                        await Http.PostAsJsonAsync(url, value);
                    }
                    else
                    {
                        await Http.PostJsonAsync(url, value);
                    }

                    iterations++;
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(duration));
            running = false;

            await job;

            if (!isWarmup)
            {
                Console.WriteLine($"{iterations} iterations in {duration}s");
            }
        }

        private enum TestMethod
        {
            Get,
            Post
        }

        private enum ClassType
        {
            Object,
            Collection
        }
    }
}
