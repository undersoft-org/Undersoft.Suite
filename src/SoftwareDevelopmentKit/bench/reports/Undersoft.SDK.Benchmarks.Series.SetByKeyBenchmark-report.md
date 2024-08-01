<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-02-29-20-37-38</title>

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
  Job-WMZDMT : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>IterationCount=5  RunStrategy=ColdStart  
</code></pre>

<table>
<thead><tr><th>                      Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>OrderedDictionary_SetByKey_Test</td><td>4,139.2 ms</td><td>3,754.28 ms</td><td>974.97 ms</td><td>6</td><td>107000.0000</td><td>26000.0000</td><td>675,424,640 B</td>
</tr><tr><td>Dictionary_SetByKey_Test</td><td>1,024.7 ms</td><td>283.61 ms</td><td>73.65 ms</td><td>4</td><td>168000.0000</td><td>-</td><td>528,012,400 B</td>
</tr><tr><td>ConcurrentDictionary_SetByKey_Test</td><td>1,276.1 ms</td><td>107.57 ms</td><td>27.93 ms</td><td>5</td><td>168000.0000</td><td>-</td><td>528,012,160 B</td>
</tr><tr><td>Chain_SetByKey_Test</td><td>212.3 ms</td><td>14.36 ms</td><td>3.73 ms</td><td>1</td><td>-</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_SetByKey_Test</td><td>286.2 ms</td><td>36.30 ms</td><td>9.43 ms</td><td>3</td><td>-</td><td>-</td><td>12,160 B</td>
</tr><tr><td>Listing_SetByKey_Test</td><td>209.6 ms</td><td>10.50 ms</td><td>2.73 ms</td><td>1</td><td>-</td><td>-</td><td>12,160 B</td>
</tr><tr><td>Registry_SetByKey_Test</td><td>271.6 ms</td><td>18.34 ms</td><td>4.76 ms</td><td>2</td><td>-</td><td>-</td><td>-</td>
</tr></tbody></table>
</body>
</html>
