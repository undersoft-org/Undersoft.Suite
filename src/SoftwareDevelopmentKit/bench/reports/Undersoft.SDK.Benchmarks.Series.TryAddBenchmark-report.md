<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-03-01-11-10-35</title>

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
  Job-STIRTN : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>IterationCount=5  RunStrategy=ColdStart  
</code></pre>

<table>
<thead><tr><th>                    Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_TryAdd_Test</td><td>701.4 ms</td><td>70.30 ms</td><td>18.26 ms</td><td>1</td><td>20000.0000</td><td>10000.0000</td><td>154 MB</td>
</tr><tr><td>Catalog_TryAdd_Test</td><td>916.9 ms</td><td>84.50 ms</td><td>21.94 ms</td><td>2</td><td>20000.0000</td><td>10000.0000</td><td>168 MB</td>
</tr><tr><td>Listing_TryAdd_Test</td><td>858.5 ms</td><td>243.29 ms</td><td>63.18 ms</td><td>2</td><td>20000.0000</td><td>10000.0000</td><td>186 MB</td>
</tr><tr><td>Registry_TryAdd_Test</td><td>1.024 s</td><td>0.4381 s</td><td>0.1138 s</td><td>1</td><td>20000.0000</td><td>10000.0000</td><td>186 MB</td>
</tr><tr><td>Dictionary_TryAdd_Test</td><td>2,057.5 ms</td><td>441.08 ms</td><td>114.55 ms</td><td>4</td><td>84000.0000</td><td>22000.0000</td><td>653 MB</td>
</tr><tr><td>ConcurrentDictionary_TryAdd_Test</td><td>5,983.9 ms</td><td>2,966.91 ms</td><td>770.50 ms</td><td>5</td><td>117000.0000</td><td>43000.0000</td><td>744 MB</td>
</tr></tbody></table>
</body>
</html>
