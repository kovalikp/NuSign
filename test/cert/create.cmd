makecert.exe ^
-n "CN=NuSingTest" ^
-r ^
-pe ^
-a sha512 ^
-len 4096 ^
-cy authority ^
-sv NuSingTest.pvk ^
NuSingTest.cer
 
pvk2pfx.exe ^
-pvk NuSingTest.pvk ^
-spc NuSingTest.cer ^
-pfx NuSingTest.pfx 