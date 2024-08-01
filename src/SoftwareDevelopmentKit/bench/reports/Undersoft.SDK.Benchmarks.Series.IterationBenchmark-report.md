<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-03-01-03-01-54</title>

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
  Job-FUIQBL : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>IterationCount=5  RunStrategy=ColdStart  
</code></pre>

<table>
<thead><tr><th>                       Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_Iteration_Test</td><td>94.42 ms</td><td>31.58 ms</td><td>8.201 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_Iteration_Test</td><td>90.14 ms</td><td>19.59 ms</td><td>5.087 ms</td><td>2</td><td>-</td><td>12,144 B</td>
</tr><tr><td>Listing_Iteration_Test</td><td>94.20 ms</td><td>28.26 ms</td><td>7.340 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Registry_Iteration_Test</td><td>95.31 ms</td><td>43.38 ms</td><td>11.266 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Dictionary_Iteration_Test</td><td>60.94 ms</td><td>36.02 ms</td><td>9.354 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>OrderedDictionary_Iteration_Test</td><td>217.26 ms</td><td>72.03 ms</td><td>18.707 ms</td><td>3</td><td>40000.0000</td><td>128,006,208 B</td>
</tr><tr><td>ConcurrentDictionary_Iteration_Test</td><td>449.44 ms</td><td>149.87 ms</td><td>38.921 ms</td><td>4</td><td>-</td><td>-</td>
</tr></tbody></table>
</body>
</html>
