<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-03-01-10-41-51</title>

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
  Job-FQTGQU : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>InvocationCount=1  IterationCount=5  RunStrategy=ColdStart  
UnrollFactor=1  
</code></pre>

<table>
<thead><tr><th>                    Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Dictionary_Remove_Test</td><td>1,006.4 ms</td><td>25.72 ms</td><td>6.68 ms</td><td>3</td><td>158000.0000</td><td>1000.0000</td><td>478 MB</td>
</tr><tr><td>ConcurrentDictionary_Remove_Test</td><td>1,497.5 ms</td><td>817.11 ms</td><td>212.20 ms</td><td>4</td><td>158000.0000</td><td>1000.0000</td><td>478 MB</td>
</tr><tr><td>Chain_Remove_Test</td><td>342.7 ms</td><td>141.28 ms</td><td>36.69 ms</td><td>1</td><td>-</td><td>-</td><td>18 MB</td>
</tr><tr><td>Catalog_Remove_Test</td><td>597.7 ms</td><td>457.69 ms</td><td>118.86 ms</td><td>2</td><td>-</td><td>-</td><td>26 MB</td>
</tr><tr><td>Listing_Remove_Test</td><td>353.0 ms</td><td>385.37 ms</td><td>100.08 ms</td><td>1</td><td>-</td><td>-</td><td>37 MB</td>
</tr><tr><td>Registry_Remove_Test</td><td>526.5 ms</td><td>82.80 ms</td><td>21.50 ms</td><td>2</td><td>-</td><td>-</td><td>37 MB</td>
</tr></tbody></table>
</body>
</html>
