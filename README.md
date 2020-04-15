# HttpJsonBenchmarks

Run these benchmarks using https://github.com/aspnet/Benchmarks/tree/master/src/BenchmarksDriver2.

## Note
you don't have to clone this repo, you need to clone https://github.com/aspnet/Benchmarks/tree/master/src/BenchmarksDriver2 instead, `BenchamrksDriver` will download the client and server apps from the configuration within the file specified in `--config`.

Example command: 

```console
dotnet run -- --config https://raw.githubusercontent.com/Jozkee/HttpJsonBenchmarks/master/benchmarks.httpjson.yml --scenario get|post.object|collection --server.endpoints http://asp-perf-lin:5001 --client.endpoints http://asp-perf-win:5001 --variable serverUri=10.0.0.102 --variable newJson=true|false
```