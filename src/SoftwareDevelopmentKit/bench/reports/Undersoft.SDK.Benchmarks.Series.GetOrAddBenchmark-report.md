<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-02-29-18-31-06</title>

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
  Job-JYJTGE : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>InvocationCount=1  IterationCount=5  RunStrategy=ColdStart  
UnrollFactor=1  
</code></pre>

<table>
<thead><tr><th>                      Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_GetOrAdd_Test</td><td>469.2 ms</td><td>56.27 ms</td><td>14.61 ms</td><td>1</td><td>10000.0000</td><td>5000.0000</td><td>79 MB</td>
</tr><tr><td>Catalog_GetOrAdd_Test</td><td>669.0 ms</td><td>100.12 ms</td><td>26.00 ms</td><td>2</td><td>10000.0000</td><td>4000.0000</td><td>87 MB</td>
</tr><tr><td>Listing_GetOrAdd_Test</td><td>716.9 ms</td><td>294.47 ms</td><td>76.47 ms</td><td>2</td><td>10000.0000</td><td>5000.0000</td><td>98 MB</td>
</tr><tr><td>Registry_GetOrAdd_Test</td><td>712.1 ms</td><td>62.87 ms</td><td>16.33 ms</td><td>1</td><td>10000.0000</td><td>4000.0000</td><td>98 MB</td>
</tr><tr><td>ConcurrentDictionary_GetOrAdd_Test</td><td>7,204.8 ms</td><td>509.62 ms</td><td>132.35 ms</td><td>3</td><td>98000.0000</td><td>34000.0000</td><td>657 MB</td>
</tr></tbody></table>
</body>
</html>
