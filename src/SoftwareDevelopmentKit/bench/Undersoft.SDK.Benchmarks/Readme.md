# Benchamrks performed and specialized for Undersoft SDK usage and purpose and for them results show that Undersoft SDK is ~ 5 times faster with ~ 5 times less RAM usage then .NET Standard Collections and Parallel Math operations on IEnumerables
### Undersoft.SDK Data Structure Algorithms VS .NET System.Collections & Parrallel Undersoft.SDK.Instant.Math VS .NET Standard Math operators using System.Linq on IEnumerables
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
</thead><tbody><tr><td>Instant_Series_Undersoft_Math_AsParallel</td><td>309.5 ms</td><td>5.76 ms</td><td>5.66 ms</td><td>1</td><td>76000</td><td>229 MB</td>
</tr><tr><td>Instant_Series_DotNet_Math_AsParellel</td><td>2,028.9 ms</td><td>39.79 ms</td><td>60.76 ms</td><td>2</td><td>535000</td><td>1,602 MB</td>
</tr><tr><td>Instant_Proxies_Undersoft_Math_AsParallel</td><td>327.7 ms</td><td>5.67 ms</td><td>6.53 ms</td><td>1</td><td>76500</td><td>229 MB</td>
</tr><tr><td>Instant_Proxies_DotNet_Math_AsParellel</td><td>2,490.4 ms</td><td>21.73 ms</td><td>18.14 ms</td><td>2</td><td>535000</td><td>1,602 MB</td>
</tr></tbody></table>

<table>
<thead><tr><th>                         Method (5 concurrent tasks each 200K objects)</th><th> Mean</th><th>Error</th><th>StdDev</th><th>Median</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Catalog_Add</td><td>1,416.55 ms</td><td>236.30 ms</td><td>61.367 ms</td><td>1,424.28 ms</td><td>6</td><td>20000</td><td>10000</td><td>199 MB</td>
</tr><tr><td>Catalog_AddOrUpdate</td><td>1,224.97 ms</td><td>178.81 ms</td><td>46.437 ms</td><td>1,216.00 ms</td><td>5</td><td>10000</td><td>5000</td><td>118 MB</td>
</tr><tr><td>Catalog_ContainsKey</td><td>60.65 ms</td><td>19.14 ms</td><td>4.971 ms</td><td>58.67 ms</td><td>1</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Catalog_GetByKey</td><td>195.04 ms</td><td>39.81 ms</td><td>10.339 ms</td><td>191.40 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Catalog_GetOrAdd</td><td>1,045.72 ms</td><td>190.98 ms</td><td>49.598 ms</td><td>1,025.00 ms</td><td>4</td><td>10000</td><td>5000</td><td>118 MB</td>
</tr><tr><td>Catalog_Itertion</td><td>191.60 ms</td><td>398.34 ms</td><td>103.448 ms</td><td>142.97 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Catalog_Remove</td><td>122.05 ms</td><td>22.19 ms</td><td>5.763 ms</td><td>119.63 ms</td><td>2</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Catalog_SetByKey</td><td>215.20 ms</td><td>36.67 ms</td><td>9.523 ms</td><td>210.73 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Catalog_TryGetByKey</td><td>197.87 ms</td><td>50.83 ms</td><td>13.200 ms</td><td>190.54 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_Add</td><td>1,532.15 ms</td><td>188.53 ms</td><td>48.961 ms</td><td>1,523.63 ms</td><td>7</td><td>21000</td><td>10000</td><td>217 MB</td>
</tr><tr><td>Registry_AddOrUpdate</td><td>1,085.43 ms</td><td>148.56 ms</td><td>38.581 ms</td><td>1,078.40 ms</td><td>4</td><td>10000</td><td>5000</td><td>128 MB</td>
</tr><tr><td>Registry_ContainsKey</td><td>60.74 ms</td><td>21.25 ms</td><td>5.519 ms</td><td>57.64 ms</td><td>1</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_GetByKey</td><td>210.57 ms</td><td>72.67 ms</td><td>18.873 ms</td><td>208.03 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_GetOrAdd</td><td>997.04 ms</td><td>105.59 ms</td><td>27.421 ms</td><td>991.91 ms</td><td>4</td><td>10000</td><td>5000</td><td>128 MB</td>
</tr><tr><td>Registry_Itertion</td><td>162.31 ms</td><td>106.05 ms</td><td>27.541 ms</td><td>146.92 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_Remove</td><td>123.73 ms</td><td>25.34 ms</td><td>6.580 ms</td><td>124.04 ms</td><td>2</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_SetByKey</td><td>208.29 ms</td><td>60.35 ms</td><td>15.672 ms</td><td>203.98 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>Registry_TryGetByKey</td><td>190.18 ms</td><td>29.59 ms</td><td>7.684 ms</td><td>188.66 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentDictionary_Add</td><td>1,979.11 ms</td><td>346.71 ms</td><td>90.040 ms</td><td>1,979.77 ms</td><td>8</td><td>84000</td><td>31000</td><td>577 MB</td>
</tr><tr><td>ConcurrentDictionary_AddOrUpdate</td><td>1,228.47 ms</td><td>202.59 ms</td><td>52.611 ms</td><td>1,209.23 ms</td><td>5</td><td>101000</td><td>28000</td><td>648 MB</td>
</tr><tr><td>ConcurrentDictionary_ContainsKey</td><td>203.32 ms</td><td>48.06 ms</td><td>12.482 ms</td><td>201.00 ms</td><td>3</td><td>107000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentDictionary_GetByKey</td><td>214.59 ms</td><td>99.61 ms</td><td>25.868 ms</td><td>220.30 ms</td><td>3</td><td>107000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentDictionary_GetOrAdd</td><td>1,082.26 ms</td><td>174.55 ms</td><td>45.330 ms</td><td>1,075.29 ms</td><td>4</td><td>69000</td><td>22000</td><td>465 MB</td>
</tr><tr><td>ConcurrentDictionary_Itertion</td><td>242.47 ms</td><td>396.37 ms</td><td>102.937 ms</td><td>181.28 ms</td><td>3</td><td>-</td><td>-</td><td>31 MB</td>
</tr><tr><td>ConcurrentDictionary_Remove</td><td>160.96 ms</td><td>73.52 ms</td><td>19.093 ms</td><td>152.38 ms</td><td>3</td><td>52000</td><td>1000</td><td>191 MB</td>
</tr><tr><td>ConcurrentDictionary_SetByKey</td><td>236.14 ms</td><td>98.28 ms</td><td>25.524 ms</td><td>222.56 ms</td><td>3</td><td>107000</td><td>-</td><td>351 MB</td>
</tr><tr><td>ConcurrentDictionary_TryGetByKey</td><td>200.36 ms</td><td>81.21 ms</td><td>21.089 ms</td><td>200.08 ms</td><td>3</td><td>107000</td><td>-</td><td>351 MB</td>
</tr></tbody></table>
<table>
<thead><tr><th>                 Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Dictionary_Add_Test</td><td>1,749.7 ms</td><td>400.47 ms</td><td>104.00 ms</td><td>5</td><td>128000</td><td>-</td><td>533 MB</td>
</tr><tr><td>OrderedDictionary_Add_Test</td><td>2,070.1 ms</td><td>269.37 ms</td><td>69.95 ms</td><td>6</td><td>61000</td><td>16000</td><td>529 MB</td>
</tr><tr><td>ConcurrentDictionary_Add_Test</td><td>4,343.0 ms</td><td>1,205.17 ms</td><td>312.98 ms</td><td>7</td><td>84000</td><td>26000</td><td>546 MB</td>
</tr><tr><td>Chain_Add_Test</td><td>719.0 ms</td><td>185.91 ms</td><td>48.28 ms</td><td>1</td><td>20000</td><td>10000</td><td>154 MB</td>
</tr><tr><td>Catalog_Add_Test</td><td>923.9 ms</td><td>95.30 ms</td><td>24.75 ms</td><td>3</td><td>20000</td><td>10000</td><td>168 MB</td>
</tr><tr><td>Listing_Add_Test</td><td>818.5 ms</td><td>130.86 ms</td><td>33.99 ms</td><td>2</td><td>20000</td><td>10000</td><td>186 MB</td>
</tr><tr><td>Registry_Add_Test</td><td>999.1 ms</td><td>139.11 ms</td><td>36.13 ms</td><td>4</td><td>20000</td><td>10000</td><td>186 MB</td>
</tr></tbody></table>

<table>
<thead><tr><th>                      Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Dictionary_GetByKey_Test</td><td>924.9 ms</td><td>73.96 ms</td><td>19.21 ms</td><td>2</td><td>168000</td><td>504 MB</td>
</tr><tr><td>OrderedDictionary_GetByKey_Test</td><td>675.6 ms</td><td>272.82 ms</td><td>70.85 ms</td><td>1</td><td>96000</td><td>290 MB</td>
</tr><tr><td>ConcurrentDictionary_GetByKey_Test</td><td>1,253.3 ms</td><td>392.78 ms</td><td>102.00 ms</td><td>3</td><td>168000</td><td>504 MB</td>
</tr><tr><td>Chain_GetByKey_Test</td><td>212.2 ms</td><td>68.58 ms</td><td>17.81 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_GetByKey_Test</td><td>273.8 ms</td><td>9.34 ms</td><td>2.43 ms</td><td>2</td><td>-</td><td>12,448 B</td>
</tr><tr><td>Listing_GetByKey_Test</td><td>206.8 ms</td><td>5.54 ms</td><td>1.44 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>Registry_GetByKey_Test</td><td>284.9 ms</td><td>57.66 ms</td><td>14.97 ms</td><td>2</td><td>-</td><td>12,160 B</td>
</tr></tbody></table>

<table>
<thead><tr><th>                      Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>OrderedDictionary_SetByKey_Test</td><td>4,139.2 ms</td><td>3,754.28 ms</td><td>974.97 ms</td><td>6</td><td>107000</td><td>26000</td><td>675,424,640 B</td>
</tr><tr><td>Dictionary_SetByKey_Test</td><td>1,024.7 ms</td><td>283.61 ms</td><td>73.65 ms</td><td>4</td><td>168000</td><td>-</td><td>528,012,400 B</td>
</tr><tr><td>ConcurrentDictionary_SetByKey_Test</td><td>1,276.1 ms</td><td>107.57 ms</td><td>27.93 ms</td><td>5</td><td>168000</td><td>-</td><td>528,012,160 B</td>
</tr><tr><td>Chain_SetByKey_Test</td><td>212.3 ms</td><td>14.36 ms</td><td>3.73 ms</td><td>1</td><td>-</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_SetByKey_Test</td><td>286.2 ms</td><td>36.30 ms</td><td>9.43 ms</td><td>3</td><td>-</td><td>-</td><td>12,160 B</td>
</tr><tr><td>Listing_SetByKey_Test</td><td>209.6 ms</td><td>10.50 ms</td><td>2.73 ms</td><td>1</td><td>-</td><td>-</td><td>12,160 B</td>
</tr><tr><td>Registry_SetByKey_Test</td><td>271.6 ms</td><td>18.34 ms</td><td>4.76 ms</td><td>2</td><td>-</td><td>-</td><td>-</td>
</tr></tbody></table>

<table>
<thead><tr><th>                         Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Dictionary_ContainsKey_Test</td><td>957.6 ms</td><td>104.59 ms</td><td>27.16 ms</td><td>3</td><td>168000</td><td>528,012,160 B</td>
</tr><tr><td>OrderedDictionary_ContainsKey_Test</td><td>678.5 ms</td><td>302.66 ms</td><td>78.60 ms</td><td>2</td><td>96000</td><td>304,006,656 B</td>
</tr><tr><td>ConcurrentDictionary_ContainsKey_Test</td><td>1,147.2 ms</td><td>125.92 ms</td><td>32.70 ms</td><td>4</td><td>168000</td><td>528,012,160 B</td>
</tr><tr><td>Chain_ContainsKey_Test</td><td>207.2 ms</td><td>28.16 ms</td><td>7.31 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_ContainsKey_Test</td><td>203.0 ms</td><td>35.18 ms</td><td>9.14 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>Listing_ContainsKey_Test</td><td>199.2 ms</td><td>53.65 ms</td><td>13.93 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>Registry_ContainsKey_Test</td><td>214.3 ms</td><td>17.66 ms</td><td>4.59 ms</td><td>1</td><td>-</td><td>-</td>
</tr></tbody></table>

<table>
<thead><tr><th>                    Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Gen 1</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Dictionary_Remove_Test</td><td>1,006.4 ms</td><td>25.72 ms</td><td>6.68 ms</td><td>3</td><td>158000</td><td>1000</td><td>478 MB</td>
</tr><tr><td>ConcurrentDictionary_Remove_Test</td><td>1,497.5 ms</td><td>817.11 ms</td><td>212.20 ms</td><td>4</td><td>158000</td><td>1000</td><td>478 MB</td>
</tr><tr><td>Chain_Remove_Test</td><td>342.7 ms</td><td>141.28 ms</td><td>36.69 ms</td><td>1</td><td>-</td><td>-</td><td>18 MB</td>
</tr><tr><td>Catalog_Remove_Test</td><td>597.7 ms</td><td>457.69 ms</td><td>118.86 ms</td><td>2</td><td>-</td><td>-</td><td>26 MB</td>
</tr><tr><td>Listing_Remove_Test</td><td>353.0 ms</td><td>385.37 ms</td><td>100.08 ms</td><td>1</td><td>-</td><td>-</td><td>37 MB</td>
</tr><tr><td>Registry_Remove_Test</td><td>526.5 ms</td><td>82.80 ms</td><td>21.50 ms</td><td>2</td><td>-</td><td>-</td><td>37 MB</td>
</tr></tbody></table>

<table>
<thead><tr><th>                 Method (10K objects)</th><th> Mean</th><th>Error</th><th>StdDev</th><th>Median</th><th>Rank</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_ContainsValue_Test</td><td>631.6 &mu;s</td><td>1,889.6 &mu;s</td><td>490.7 &mu;s</td><td>403.1 &mu;s</td><td>1</td><td>70,160 B</td>
</tr><tr><td>Catalog_ContainsValue_Test</td><td>837.1 &mu;s</td><td>3,238.9 &mu;s</td><td>841.1 &mu;s</td><td>494.4 &mu;s</td><td>1</td><td>69,872 B</td>
</tr><tr><td>Listing_ContainsValue_Test</td><td>832.6 &mu;s</td><td>2,829.0 &mu;s</td><td>734.7 &mu;s</td><td>548.9 &mu;s</td><td>1</td><td>70,400 B</td>
</tr><tr><td>Registry_ContainsValue_Test</td><td>1,622.2 &mu;s</td><td>4,626.7 &mu;s</td><td>1,201.5 &mu;s</td><td>971.4 &mu;s</td><td>2</td><td>70,112 B</td>
</tr><tr><td>List_ContainsValue_Test</td><td>23,234.8 &mu;s</td><td>10,714.2 &mu;s</td><td>2,782.4 &mu;s</td><td>23,374.9 &mu;s</td><td>3</td><td>-</td>
</tr><tr><td>Dictionary_ContainsValue_Test</td><td>24,653.2 &mu;s</td><td>37,045.9 &mu;s</td><td>9,620.7 &mu;s</td><td>19,812.0 &mu;s</td><td>3</td><td>-</td>
</tr></tbody></table>
<table>
<thead><tr><th>                       Method (1 mln  objects)</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Rank</th><th>Gen 0</th><th>Allocated</th>
</tr>
</thead><tbody><tr><td>Chain_Iteration_Test</td><td>94.42 ms</td><td>31.58 ms</td><td>8.201 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Catalog_Iteration_Test</td><td>90.14 ms</td><td>19.59 ms</td><td>5.087 ms</td><td>2</td><td>-</td><td>12,144 B</td>
</tr><tr><td>Listing_Iteration_Test</td><td>94.20 ms</td><td>28.26 ms</td><td>7.340 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Registry_Iteration_Test</td><td>95.31 ms</td><td>43.38 ms</td><td>11.266 ms</td><td>2</td><td>-</td><td>-</td>
</tr><tr><td>Dictionary_Iteration_Test</td><td>60.94 ms</td><td>36.02 ms</td><td>9.354 ms</td><td>1</td><td>-</td><td>-</td>
</tr><tr><td>OrderedDictionary_Iteration_Test</td><td>217.26 ms</td><td>72.03 ms</td><td>18.707 ms</td><td>3</td><td>40000</td><td>128,006,208 B</td>
</tr><tr><td>ConcurrentDictionary_Iteration_Test</td><td>449.44 ms</td><td>149.87 ms</td><td>38.921 ms</td><td>4</td><td>-</td><td>-</td>
</tr></tbody></table>
</body>
</html>
