<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-03-01-10-59-31</title>

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
  Job-IHUXZY : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>IterationCount=5  RunStrategy=ColdStart  
</code></pre>

<table>
<thead><tr><th>                 Method</th><th> Mean</th><th>Error</th><th>StdDev</th><th>Median</th><th>Rank</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_ContainsValue_Test</td><td>631.6 &mu;s</td><td>1,889.6 &mu;s</td><td>490.7 &mu;s</td><td>403.1 &mu;s</td><td>1</td><td>70,160 B</td>
</tr><tr><td>Catalog_ContainsValue_Test</td><td>837.1 &mu;s</td><td>3,238.9 &mu;s</td><td>841.1 &mu;s</td><td>494.4 &mu;s</td><td>1</td><td>69,872 B</td>
</tr><tr><td>Listing_ContainsValue_Test</td><td>832.6 &mu;s</td><td>2,829.0 &mu;s</td><td>734.7 &mu;s</td><td>548.9 &mu;s</td><td>1</td><td>70,400 B</td>
</tr><tr><td>Registry_ContainsValue_Test</td><td>1,622.2 &mu;s</td><td>4,626.7 &mu;s</td><td>1,201.5 &mu;s</td><td>971.4 &mu;s</td><td>2</td><td>70,112 B</td>
</tr><tr><td>List_ContainsValue_Test</td><td>23,234.8 &mu;s</td><td>10,714.2 &mu;s</td><td>2,782.4 &mu;s</td><td>23,374.9 &mu;s</td><td>3</td><td>-</td>
</tr><tr><td>Dictionary_ContainsValue_Test</td><td>24,653.2 &mu;s</td><td>37,045.9 &mu;s</td><td>9,620.7 &mu;s</td><td>19,812.0 &mu;s</td><td>3</td><td>-</td>
</tr></tbody></table>
</body>
</html>
