<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>Undersoft.SDK.Benchmarks.Instant.Math.InstantMathBenchmark-20240226-210455</title>

<style type="text/css">
	table { border-collapse: collapse; display: block; width: 100%; overflow: auto; }
	td, th { padding: 6px 13px; border: 1px solid #ddd; text-align: right; }
	tr { background-color: #fff; border-top: 1px solid #ccc; }
	tr:nth-child(even) { background: #f8f8f8; }
</style>
</head>
<body>
<pre><code>
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i7-4700MQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code></code></pre>

<table>
<thead><tr><th>                                                                                                                                                                       Method (10 mln objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Instant_Series_Undersoft_Math_AsParallel</td><td>309.5 ms</td><td>5.76 ms</td><td>5.66 ms</td><td>1</td><td>76000.0000</td><td>229 MB</td>
</tr><tr><td>Instant_Series_DotNet_Math_AsParellel</td><td>2,028.9 ms</td><td>39.79 ms</td><td>60.76 ms</td><td>2</td><td>535000.0000</td><td>1,602 MB</td>
</tr><tr><td>Instant_Proxies_Undersoft_Math_AsParallel</td><td>327.7 ms</td><td>5.67 ms</td><td>6.53 ms</td><td>1</td><td>76500.0000</td><td>229 MB</td>
</tr><tr><td>Instant_Proxies_DotNet_Math_AsParellel</td><td>2,490.4 ms</td><td>21.73 ms</td><td>18.14 ms</td><td>2</td><td>535000.0000</td><td>1,602 MB</td>
</tr></tbody></table>
</body>
</html>
