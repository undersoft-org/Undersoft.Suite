<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='utf-8' />
<title>BenchmarkRun-joined-2024-02-29-08-30-12</title>

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
  Job-NKBGFT : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT
</code></pre>
<pre><code>InvocationCount=1  IterationCount=5  RunStrategy=ColdStart  
UnrollFactor=1  
</code></pre>

<table>
<thead><tr><th>                    Type</th><th>                         Method</th><th> Mean</th><th>Error</th><th>StdDev</th><th>Median</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>ConcurrentAddBenchmark</td><td>Catalog_Add_Test</td><td>1,416.55 ms</td><td>236.30 ms</td><td>61.367 ms</td><td>1,424.28 ms</td><td>6</td><td>20000.0000</td><td>10000.0000</td><td>199 MB</td>
</tr><tr><td>ConcurrentAddOrUpdateBenchmark</td><td>Catalog_AddOrUpdate_Test</td><td>1,224.97 ms</td><td>178.81 ms</td><td>46.437 ms</td><td>1,216.00 ms</td><td>5</td><td>10000.0000</td><td>5000.0000</td><td>118 MB</td>
</tr><tr><td>ConcurrentContainsKeyBenchmark</td><td>Catalog_ContainsKey_Test</td><td>60.65 ms</td><td>19.14 ms</td><td>4.971 ms</td><td>58.67 ms</td><td>1</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentGetByKeyBenchmark</td><td>Catalog_GetByKey_Test</td><td>195.04 ms</td><td>39.81 ms</td><td>10.339 ms</td><td>191.40 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentGetOrAddBenchmark</td><td>Catalog_GetOrAdd_Test</td><td>1,045.72 ms</td><td>190.98 ms</td><td>49.598 ms</td><td>1,025.00 ms</td><td>4</td><td>10000.0000</td><td>5000.0000</td><td>118 MB</td>
</tr><tr><td>ConcurrentIterationBenchmark</td><td>Catalog_Itertion_Test</td><td>191.60 ms</td><td>398.34 ms</td><td>103.448 ms</td><td>142.97 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentRemoveBenchmark</td><td>Catalog_Remove_Test</td><td>122.05 ms</td><td>22.19 ms</td><td>5.763 ms</td><td>119.63 ms</td><td>2</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentSetByKeyBenchmark</td><td>Catalog_SetByKey_Test</td><td>215.20 ms</td><td>36.67 ms</td><td>9.523 ms</td><td>210.73 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentTryGetByKeyBenchmark</td><td>Catalog_TryGetByKey_Test</td><td>197.87 ms</td><td>50.83 ms</td><td>13.200 ms</td><td>190.54 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentAddBenchmark</td><td>Registry_Add_Test</td><td>1,532.15 ms</td><td>188.53 ms</td><td>48.961 ms</td><td>1,523.63 ms</td><td>7</td><td>21000.0000</td><td>10000.0000</td><td>217 MB</td>
</tr><tr><td>ConcurrentAddOrUpdateBenchmark</td><td>Registry_AddOrUpdate_Test</td><td>1,085.43 ms</td><td>148.56 ms</td><td>38.581 ms</td><td>1,078.40 ms</td><td>4</td><td>10000.0000</td><td>5000.0000</td><td>128 MB</td>
</tr><tr><td>ConcurrentContainsKeyBenchmark</td><td>Registry_ContainsKey_Test</td><td>60.74 ms</td><td>21.25 ms</td><td>5.519 ms</td><td>57.64 ms</td><td>1</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentGetByKeyBenchmark</td><td>Registry_GetByKey_Test</td><td>210.57 ms</td><td>72.67 ms</td><td>18.873 ms</td><td>208.03 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentGetOrAddBenchmark</td><td>Registry_GetOrAdd_Test</td><td>997.04 ms</td><td>105.59 ms</td><td>27.421 ms</td><td>991.91 ms</td><td>4</td><td>10000.0000</td><td>5000.0000</td><td>128 MB</td>
</tr><tr><td>ConcurrentIterationBenchmark</td><td>Registry_Itertion_Test</td><td>162.31 ms</td><td>106.05 ms</td><td>27.541 ms</td><td>146.92 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentRemoveBenchmark</td><td>Registry_Remove_Test</td><td>123.73 ms</td><td>25.34 ms</td><td>6.580 ms</td><td>124.04 ms</td><td>2</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentSetByKeyBenchmark</td><td>Registry_SetByKey_Test</td><td>208.29 ms</td><td>60.35 ms</td><td>15.672 ms</td><td>203.98 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentTryGetByKeyBenchmark</td><td>Registry_TryGetByKey_Test</td><td>190.18 ms</td><td>29.59 ms</td><td>7.684 ms</td><td>188.66 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentAddBenchmark</td><td>ConcurrentDictionary_Add_Test</td><td>1,979.11 ms</td><td>346.71 ms</td><td>90.040 ms</td><td>1,979.77 ms</td><td>8</td><td>84000.0000</td><td>31000.0000</td><td>577 MB</td>
</tr><tr><td>ConcurrentAddOrUpdateBenchmark</td><td>ConcurrentDictionary_AddOrUpdate_Test</td><td>1,228.47 ms</td><td>202.59 ms</td><td>52.611 ms</td><td>1,209.23 ms</td><td>5</td><td>101000.0000</td><td>28000.0000</td><td>648 MB</td>
</tr><tr><td>ConcurrentContainsKeyBenchmark</td><td>ConcurrentDictionary_ContainsKey_Test</td><td>203.32 ms</td><td>48.06 ms</td><td>12.482 ms</td><td>201.00 ms</td><td>3</td><td>107000.0000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentGetByKeyBenchmark</td><td>ConcurrentDictionary_GetByKey_Test</td><td>214.59 ms</td><td>99.61 ms</td><td>25.868 ms</td><td>220.30 ms</td><td>3</td><td>107000.0000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentGetOrAddBenchmark</td><td>ConcurrentDictionary_GetOrAdd_Test</td><td>1,082.26 ms</td><td>174.55 ms</td><td>45.330 ms</td><td>1,075.29 ms</td><td>4</td><td>69000.0000</td><td>22000.0000</td><td>465 MB</td>
</tr><tr><td>ConcurrentIterationBenchmark</td><td>ConcurrentDictionary_Itertion_Test</td><td>242.47 ms</td><td>396.37 ms</td><td>102.937 ms</td><td>181.28 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentRemoveBenchmark</td><td>ConcurrentDictionary_Remove_Test</td><td>160.96 ms</td><td>73.52 ms</td><td>19.093 ms</td><td>152.38 ms</td><td>3</td><td>52000.0000</td><td>1000.0000</td><td>191 MB</td>
</tr><tr><td>ConcurrentSetByKeyBenchmark</td><td>ConcurrentDictionary_SetByKey_Test</td><td>236.14 ms</td><td>98.28 ms</td><td>25.524 ms</td><td>222.56 ms</td><td>3</td><td>107000.0000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentTryGetByKeyBenchmark</td><td>ConcurrentDictionary_TryGetByKey_Test</td><td>200.36 ms</td><td>81.21 ms</td><td>21.089 ms</td><td>200.08 ms</td><td>3</td><td>107000.0000</td><td>-</td><td>351 MB</td>
</tr></tbody></table>
</body>
</html>
