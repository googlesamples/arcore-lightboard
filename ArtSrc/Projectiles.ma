//Maya ASCII 2018 scene
//Name: Projectiles.ma
//Last modified: Fri, Mar 30, 2018 02:23:38 PM
//Codeset: UTF-8
requires maya "2018";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201706261615-f9658c4cfc";
fileInfo "osv" "Mac OS X 10.13.3";
createNode transform -s -n "persp";
	rename -uid "D11A0D4C-E54F-582D-63EE-ECB4713070EE";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 271.09826065188497 495.97285234749688 274.92317464808559 ;
	setAttr ".r" -type "double3" -57.938352729440709 30.6000000000029 1.8475655781177749e-15 ;
	setAttr ".rp" -type "double3" 0 -7.1054273576010019e-15 -5.6843418860808015e-14 ;
	setAttr ".rpt" -type "double3" -2.1793122640832981e-14 -2.0722670352259202e-14 1.0192465829731312e-13 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "9A163ED9-374B-18B6-F990-7FA36528C394";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999986;
	setAttr ".coi" 672.37992183967344;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" 0 -24.999996274709702 1.9073486328125e-06 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	rename -uid "E8506896-184E-F8AA-49B8-A59AE057A168";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -1.7813022754237977 1000.1 -44.686896421905452 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "B3DC40AD-764C-2574-9FFA-3DBE3690F55C";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 771.48319192617635;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	rename -uid "CDCEAA18-4B49-FF35-9043-99A94496C520";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 33.94615610322105 50.720487229988592 1001.4758721406638 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "89ADCDA8-BF4C-2B5A-52BD-40B689A766CF";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1050.8371850068747;
	setAttr ".ow" 298.06380968681901;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".tp" -type "double3" 0 53.75632232865064 -49.361312866210938 ;
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	rename -uid "BB3FD4A9-5345-E2BB-AA77-2A82ABC5E218";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1004.3516232379329 26.198530197143555 1.9073486548393248e-05 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "FE6DB0F7-5246-DD0F-C432-728D86BD7F25";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1004.3771511920343;
	setAttr ".ow" 274.37028775513153;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".tp" -type "double3" -0.0255279541015625 26.198530197143555 1.9073486328125e-05 ;
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "Slice4";
	rename -uid "D2979B05-984A-11CF-0F47-AB8C06A1D1F5";
createNode mesh -n "SliceShape4" -p "Slice4";
	rename -uid "B91FABF2-A74D-FC43-C693-549610436C49";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.2671954520046711 0.51691456139087677 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 38 ".uvst[0].uvsp[0:37]" -type "float2" 0.084636681 0.62385494
		 0.051852681 0.57479024 0.1915772 0.51691449 0.04034052 0.51691449 0.13370135 0.65663892
		 0.051852755 0.45903876 0.19157714 0.66815114 0.084636725 0.40997404 0.13370144 0.37719005
		 0.19157723 0.36567786 0.48253816 0.57479036 0.4497543 0.62385505 0.34281379 0.51691461
		 0.40068954 0.65663898 0.49405038 0.51691467 0.34281379 0.66815126 0.48253822 0.45903885
		 0.4497543 0.40997422 0.40068954 0.37719017 0.34281385 0.36567795 0.31899878 0.97099334
		 0.25503081 0.9709934 0.25503081 0.80383706 0.31899878 0.80383712 0.19106303 0.9709934
		 0.19106303 0.80383712 0.37322816 0.9709934 0.37322813 0.80383712 0.13683362 0.9709934
		 0.13683358 0.80383712 0.40946308 0.9709934 0.40946317 0.80383706 0.10059867 0.9709934
		 0.1005986 0.80383706 0.036630832 0.97099352 0.036630668 0.80383718 0.4734309 0.9709934
		 0.4734309 0.80383706;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 20 ".pt[0:19]" -type "float3"  -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0;
	setAttr -s 20 ".vt[0:19]"  -25 -46.1939888 -19.13414383 -25 -35.35535812 -35.35531616
		 -25 -19.13419533 -46.1939621 -25 -2.5331974e-05 -49.99999619 -25 7.4505806e-06 50
		 -25 -19.13416481 46.19397736 -25 -35.35533524 35.35534286 -25 -46.19397354 19.13417244
		 -25 -50 -1.6653345e-14 25 -46.1939888 -19.13414383 25 -35.35535812 -35.35531616 25 -19.13419533 -46.1939621
		 25 -2.5331974e-05 -49.99999619 25 7.4505806e-06 50 25 -19.13416481 46.19397736 25 -35.35533524 35.35534286
		 25 -46.19397354 19.13417244 25 -50 -5.5511151e-15 -25 0 -5.5511151e-15 25 0 5.5511151e-15;
	setAttr -s 44 ".ed[0:43]"  0 1 0 1 2 0 2 3 0 4 5 0 5 6 0 6 7 0 7 8 0
		 8 0 0 9 10 0 10 11 0 11 12 0 13 14 0 14 15 0 15 16 0 16 17 0 17 9 0 0 9 1 1 10 1
		 2 11 1 3 12 0 4 13 0 5 14 1 6 15 1 7 16 1 8 17 1 18 0 1 18 1 1 18 2 1 18 3 0 18 4 0
		 18 5 1 18 6 1 18 7 1 18 8 1 9 19 1 10 19 1 11 19 1 12 19 0 13 19 0 14 19 1 15 19 1
		 16 19 1 17 19 1 18 19 1;
	setAttr -s 26 -ch 88 ".fc[0:25]" -type "polyFaces" 
		f 4 0 17 -9 -17
		mu 0 4 24 28 29 25
		f 4 1 18 -10 -18
		mu 0 4 28 32 33 29
		f 4 2 19 -11 -19
		mu 0 4 32 34 35 33
		f 4 3 21 -12 -21
		mu 0 4 36 30 31 37
		f 4 4 22 -13 -22
		mu 0 4 30 26 27 31
		f 4 5 23 -14 -23
		mu 0 4 26 20 23 27
		f 4 6 24 -15 -24
		mu 0 4 20 21 22 23
		f 4 7 16 -16 -25
		mu 0 4 21 24 25 22
		f 3 -1 -26 26
		mu 0 3 0 1 2
		f 3 -2 -27 27
		mu 0 3 4 0 2
		f 3 -3 -28 28
		mu 0 3 6 4 2
		f 3 -4 -30 30
		mu 0 3 8 9 2
		f 3 -5 -31 31
		mu 0 3 7 8 2
		f 3 -6 -32 32
		mu 0 3 5 7 2
		f 3 -7 -33 33
		mu 0 3 3 5 2
		f 3 -8 -34 25
		mu 0 3 1 3 2
		f 3 8 35 -35
		mu 0 3 10 11 12
		f 3 9 36 -36
		mu 0 3 11 13 12
		f 3 10 37 -37
		mu 0 3 13 15 12
		f 3 11 39 -39
		mu 0 3 19 18 12
		f 3 12 40 -40
		mu 0 3 18 17 12
		f 3 13 41 -41
		mu 0 3 17 16 12
		f 3 14 42 -42
		mu 0 3 16 14 12
		f 3 15 34 -43
		mu 0 3 14 10 12
		f 4 -20 -29 43 -38
		mu 0 4 15 6 2 12
		f 4 -44 29 20 38
		mu 0 4 12 2 9 19;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "Slice1";
	rename -uid "3F4B4F75-C94A-555F-FEE5-93B9B2A02EDF";
createNode mesh -n "SliceShape1" -p "Slice1";
	rename -uid "2B51986E-004B-87FF-20F2-9F8692441D30";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.19157722592353821 0.36567786335945129 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode mesh -n "polySurfaceShape1" -p "Slice1";
	rename -uid "6144BCEB-4146-0E4D-F1C9-4C8D9CAAD0D6";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.19157722592353821 0.36567786335945129 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 16 ".uvst[0].uvsp[0:15]" -type "float2" 0.084636696 0.62385494
		 0.1915772 0.51691449 0.13370135 0.65663892 0.19157714 0.66815114 0.19157717 0.62385488
		 0.4497543 0.62385505 0.34281379 0.51691461 0.40068954 0.65663898 0.34281379 0.66815126
		 0.34281385 0.36567795 0.13683362 0.9709934 0.13683358 0.80383712 0.10059867 0.9709934
		 0.1005986 0.80383706 0.036630832 0.97099352 0.036630668 0.80383718;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 20 ".pt[0:19]" -type "float3"  -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 -35.355366 -85.355316 -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0;
	setAttr -s 10 ".vt[0:9]"  -25 -35.35535812 -35.35531616 -25 -19.13419533 -46.1939621
		 -25 -2.5331974e-05 -49.99999619 -25 7.4505806e-06 50 25 -35.35535812 -35.35531616
		 25 -19.13419533 -46.1939621 25 -2.5331974e-05 -49.99999619 25 7.4505806e-06 50 -25 0 -5.5511151e-15
		 25 0 5.5511151e-15;
	setAttr -s 17 ".ed[0:16]"  0 1 0 1 2 0 4 5 0 5 6 0 0 4 0 1 5 1 2 6 0
		 3 7 0 8 0 0 8 1 1 8 2 0 8 3 0 4 9 0 5 9 1 6 9 0 7 9 0 8 9 1;
	setAttr -s 8 -ch 28 ".fc[0:7]" -type "polyFaces" 
		f 4 0 5 -3 -5
		mu 0 4 10 12 13 11
		f 4 1 6 -4 -6
		mu 0 4 12 14 15 13
		f 3 -1 -9 9
		mu 0 3 2 0 1
		f 3 -2 -10 10
		mu 0 3 3 2 1
		f 3 2 13 -13
		mu 0 3 5 7 6
		f 3 3 14 -14
		mu 0 3 7 8 6
		f 4 -7 -11 16 -15
		mu 0 4 8 3 1 6
		f 4 -17 11 7 15
		mu 0 4 6 1 4 9;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "Slice3";
	rename -uid "3E9CA983-614B-BB2E-B9CE-299F5A2F69F1";
createNode mesh -n "SliceShape3" -p "Slice3";
	rename -uid "115677D8-0446-031C-A9A0-F49C65EE53FD";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.27903381362557411 0.66833563148975372 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode mesh -n "polySurfaceShape2" -p "Slice3";
	rename -uid "2BE9E2A0-E447-66EC-3621-5DA6D834FBCC";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.27903381362557411 0.66833563148975372 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 32 ".uvst[0].uvsp[0:31]" -type "float2" 0.084636681 0.62385494
		 0.051852681 0.57479024 0.1915772 0.51691449 0.04034052 0.51691449 0.13370135 0.65663892
		 0.051852755 0.45903876 0.19157714 0.66815114 0.084636718 0.40997407 0.19157723 0.40997404
		 0.48253816 0.57479036 0.4497543 0.62385505 0.34281379 0.51691461 0.40068954 0.65663898
		 0.49405038 0.51691467 0.34281379 0.66815126 0.48253822 0.45903885 0.4497543 0.40997422
		 0.34281385 0.36567795 0.31899878 0.97099334 0.25503081 0.9709934 0.25503081 0.80383706
		 0.31899878 0.80383712 0.19106303 0.9709934 0.19106303 0.80383712 0.37322813 0.9709934
		 0.37322813 0.80383712 0.13683362 0.9709934 0.13683358 0.80383712 0.10059867 0.9709934
		 0.1005986 0.80383706 0.036630832 0.97099352 0.036630668 0.80383718;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 20 ".pt[0:19]" -type "float3"  -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 -35.355343 -14.644657 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 0 0;
	setAttr -s 18 ".vt[0:17]"  -25 -46.1939888 -19.13414383 -25 -35.35535812 -35.35531616
		 -25 -19.13419533 -46.1939621 -25 -2.5331974e-05 -49.99999619 -25 7.4505806e-06 50
		 -25 -35.35533524 35.35534286 -25 -46.19397354 19.13417244 -25 -50 -1.6653345e-14
		 25 -46.1939888 -19.13414383 25 -35.35535812 -35.35531616 25 -19.13419533 -46.1939621
		 25 -2.5331974e-05 -49.99999619 25 7.4505806e-06 50 25 -35.35533524 35.35534286 25 -46.19397354 19.13417244
		 25 -50 -5.5511151e-15 -25 0 -5.5511151e-15 25 0 5.5511151e-15;
	setAttr -s 37 ".ed[0:36]"  0 1 0 1 2 0 2 3 0 5 6 0 6 7 0 7 0 0 8 9 0
		 9 10 0 10 11 0 13 14 0 14 15 0 15 8 0 0 8 1 1 9 1 2 10 1 3 11 0 4 12 0 5 13 0 6 14 1
		 7 15 1 16 0 1 16 1 1 16 2 1 16 3 0 16 4 0 16 5 0 16 6 1 16 7 1 8 17 1 9 17 1 10 17 1
		 11 17 0 12 17 0 13 17 0 14 17 1 15 17 1 16 17 1;
	setAttr -s 20 -ch 68 ".fc[0:19]" -type "polyFaces" 
		f 4 0 13 -7 -13
		mu 0 4 22 26 27 23
		f 4 1 14 -8 -14
		mu 0 4 26 28 29 27
		f 4 2 15 -9 -15
		mu 0 4 28 30 31 29
		f 4 3 18 -10 -18
		mu 0 4 24 18 21 25
		f 4 4 19 -11 -19
		mu 0 4 18 19 20 21
		f 4 5 12 -12 -20
		mu 0 4 19 22 23 20
		f 3 -1 -21 21
		mu 0 3 0 1 2
		f 3 -2 -22 22
		mu 0 3 4 0 2
		f 3 -3 -23 23
		mu 0 3 6 4 2
		f 3 -4 -26 26
		mu 0 3 5 7 2
		f 3 -5 -27 27
		mu 0 3 3 5 2
		f 3 -6 -28 20
		mu 0 3 1 3 2
		f 3 6 29 -29
		mu 0 3 9 10 11
		f 3 7 30 -30
		mu 0 3 10 12 11
		f 3 8 31 -31
		mu 0 3 12 14 11
		f 3 9 34 -34
		mu 0 3 16 15 11
		f 3 10 35 -35
		mu 0 3 15 13 11
		f 3 11 28 -36
		mu 0 3 13 9 11
		f 4 -16 -24 36 -32
		mu 0 4 14 6 2 11
		f 4 -37 24 16 32
		mu 0 4 11 2 8 17;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "Slice2";
	rename -uid "06F93269-FB49-60D7-34A1-1C8D82DD1066";
createNode mesh -n "SliceShape2" -p "Slice2";
	rename -uid "89BD2CAC-5042-CE48-72E3-FBA88D57DB2C";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.2671954520046711 0.66833563148975372 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode mesh -n "polySurfaceShape3" -p "Slice2";
	rename -uid "128B928E-F940-49E6-9993-AFB30B446AD5";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.2671954520046711 0.66833563148975372 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 24 ".uvst[0].uvsp[0:23]" -type "float2" 0.084636681 0.62385494
		 0.051852681 0.57479024 0.1915772 0.51691449 0.040340528 0.51691449 0.13370135 0.65663892
		 0.19157714 0.66815114 0.1915772 0.51691449 0.48253816 0.57479036 0.4497543 0.62385505
		 0.34281379 0.51691461 0.40068954 0.65663898 0.49405038 0.51691467 0.34281379 0.66815126
		 0.34281385 0.36567795 0.25503081 0.97099334 0.25503081 0.80383706 0.19106303 0.9709934
		 0.19106303 0.80383712 0.13683362 0.9709934 0.13683358 0.80383712 0.10059867 0.9709934
		 0.1005986 0.80383706 0.036630832 0.97099352 0.036630668 0.80383718;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 20 ".pt[0:19]" -type "float3"  -1.0658141e-14 0 0 -1.0658141e-14 
		0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 -50.000008 -50 -1.0658141e-14 
		0 -3.4665501e-16 -1.0658141e-14 0 0 -1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 
		0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 1.0658141e-14 0 0 -1.0658141e-14 0 0 1.0658141e-14 
		0 0;
	setAttr -s 14 ".vt[0:13]"  -25 -46.1939888 -19.13414383 -25 -35.35535812 -35.35531616
		 -25 -19.13419533 -46.1939621 -25 -2.5331974e-05 -49.99999619 -25 7.4505806e-06 50
		 -25 -50 -1.6653345e-14 25 -46.1939888 -19.13414383 25 -35.35535812 -35.35531616 25 -19.13419533 -46.1939621
		 25 -2.5331974e-05 -49.99999619 25 7.4505806e-06 50 25 -50 -5.5511151e-15 -25 0 -5.5511151e-15
		 25 0 5.5511151e-15;
	setAttr -s 27 ".ed[0:26]"  0 1 0 1 2 0 2 3 0 5 0 0 6 7 0 7 8 0 8 9 0
		 11 6 0 0 6 1 1 7 1 2 8 1 3 9 0 4 10 0 5 11 0 12 0 1 12 1 1 12 2 1 12 3 0 12 4 0 12 5 0
		 6 13 1 7 13 1 8 13 1 9 13 0 10 13 0 11 13 0 12 13 1;
	setAttr -s 14 -ch 48 ".fc[0:13]" -type "polyFaces" 
		f 4 0 9 -5 -9
		mu 0 4 16 18 19 17
		f 4 1 10 -6 -10
		mu 0 4 18 20 21 19
		f 4 2 11 -7 -11
		mu 0 4 20 22 23 21
		f 4 3 8 -8 -14
		mu 0 4 14 16 17 15
		f 3 -1 -15 15
		mu 0 3 0 1 2
		f 3 -2 -16 16
		mu 0 3 4 0 2
		f 3 -3 -17 17
		mu 0 3 5 4 2
		f 3 -4 -20 14
		mu 0 3 1 3 2
		f 3 4 21 -21
		mu 0 3 7 8 9
		f 3 5 22 -22
		mu 0 3 8 10 9
		f 3 6 23 -23
		mu 0 3 10 12 9
		f 3 7 20 -26
		mu 0 3 11 7 9
		f 4 -12 -18 26 -24
		mu 0 4 12 5 2 9
		f 4 -27 18 12 24
		mu 0 4 9 2 6 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode lambert -n "lambert2";
	rename -uid "9B75F6D4-C140-95F7-1AC0-4EB800A9D821";
createNode shadingEngine -n "MechanicalLauncherBaseSG";
	rename -uid "C7EE1870-2948-749A-9CC9-85A940FB5F73";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
	rename -uid "391C5FED-024E-D49B-9488-15B1033B8647";
createNode lambert -n "lambert3";
	rename -uid "6B5EEC54-EB46-8006-DC4C-68896CE60C7C";
createNode shadingEngine -n "LauncherBarSG";
	rename -uid "7C446007-5B47-A6D4-59FE-BA95E769287B";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo2";
	rename -uid "AFCA6287-1846-552B-DB17-A989B2D66FCD";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "155997CA-4040-3ECD-B179-02A9071C17F3";
	setAttr -s 6 ".lnk";
	setAttr -s 6 ".slnk";
createNode displayLayerManager -n "layerManager";
	rename -uid "B7BD4C14-314E-C546-F3A0-F79EECA4F4AD";
createNode displayLayer -n "defaultLayer";
	rename -uid "D620E4AE-DC47-72EA-5F7E-4388EC50E65C";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "2D18DF35-A141-5517-2C11-C5BA5BA05A40";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "1DEE649D-EC48-41A4-9C0F-80AF5F4BD2CF";
	setAttr ".g" yes;
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "8ED44A9A-B144-4974-A1B4-299890EDBAD8";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "B9B280C3-AD4B-CCEB-89E6-0B9E450C8261";
createNode lambert -n "MatLauncher";
	rename -uid "A37FD63B-3E47-D3CA-EC8D-84B96CD314E3";
createNode shadingEngine -n "lambert4SG";
	rename -uid "5FB886AA-244D-8CD0-90C3-33A4BBE27211";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo3";
	rename -uid "24FFDF7A-364A-88E3-5D7F-E59F9A630B8E";
createNode script -n "uiConfigurationScriptNode";
	rename -uid "6825343C-BF44-09C3-6666-D1B2AC156A25";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n"
		+ "            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n"
		+ "            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n"
		+ "            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 449\n            -height 291\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 449\n            -height 290\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 449\n            -height 290\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1080\n            -height 625\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n"
		+ "            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n"
		+ "            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n"
		+ "            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n"
		+ "                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n"
		+ "                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n"
		+ "                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -highlightConnections 0\n                -copyConnectionsOnPaste 0\n                -connectionStyle \"bezier\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n"
		+ "                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1080\\n    -height 625\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1080\\n    -height 625\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 100 -size 500 -divisions 2 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "83D66ED0-4743-A4B6-1C99-AC8D2CEC0C55";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode lambert -n "MatSlice";
	rename -uid "32F71311-6840-86E9-29BE-3896D93ABE6A";
createNode shadingEngine -n "lambert5SG";
	rename -uid "C2B8EC32-FF45-D581-67B4-C39C66C2CF8B";
	setAttr ".ihi" 0;
	setAttr -s 4 ".dsm";
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo4";
	rename -uid "F7D96B0E-D24C-7C7A-B12D-508A5DFEF3D8";
createNode file -n "file1";
	rename -uid "12A4A141-1946-8694-4320-B88099908B97";
	setAttr ".ftn" -type "string" "/Users/briancollins/projects/ARtillery/Assets/Game/Textures/CylindericalLauncher.psd";
	setAttr ".cs" -type "string" "sRGB";
createNode place2dTexture -n "place2dTexture1";
	rename -uid "06BE2DC0-8447-1F1A-9441-65B0DD858599";
createNode polyMergeVert -n "polyMergeVert1";
	rename -uid "2AC8B36C-B740-D6AE-D35B-BE82F8760F28";
	setAttr ".ics" -type "componentList" 2 "vtx[0]" "vtx[3]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 63.045361123929744 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweakUV -n "polyTweakUV1";
	rename -uid "2B71CF26-F142-F1C6-11E1-FAAADC7DA340";
	setAttr ".uopa" yes;
	setAttr -s 4 ".uvtk";
	setAttr ".uvtk[5]" -type "float2" -1.2850975e-08 6.5088939e-09 ;
	setAttr ".uvtk[9]" -type "float2" -1.0150589e-07 0.25817704 ;
	setAttr ".uvtk[11]" -type "float2" -1.2007925e-08 4.9056155e-09 ;
createNode polyMergeVert -n "polyMergeVert2";
	rename -uid "0B46C531-0746-FB3C-881B-EBA0E92AAFC1";
	setAttr ".ics" -type "componentList" 2 "vtx[3]" "vtx[6]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 63.045361123929744 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweak -n "polyTweak1";
	rename -uid "515163E9-A54D-6FA5-5ABE-B4ADAF7EF8B0";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk";
	setAttr ".tk[6]" -type "float3" 0 -35.355366 -85.355316 ;
createNode polyMergeVert -n "polyMergeVert3";
	rename -uid "0D4344A1-D34C-7EDB-BB12-FE8083A1640B";
	setAttr ".ics" -type "componentList" 1 "vtx[4:5]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 127.40734650595698 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweakUV -n "polyTweakUV2";
	rename -uid "552AB6FD-3548-A6A4-BDA7-C59A69EB32A7";
	setAttr ".uopa" yes;
	setAttr -s 4 ".uvtk";
	setAttr ".uvtk[16]" -type "float2" 4.5298285e-09 1.4713581e-08 ;
	setAttr ".uvtk[17]" -type "float2" -4.1901242e-08 0.044296198 ;
	setAttr ".uvtk[25]" -type "float2" -9.6411608e-09 9.0214876e-09 ;
createNode polyMergeVert -n "polyMergeVert4";
	rename -uid "FB54ECC0-FB4C-56ED-B721-819F3136BA95";
	setAttr ".ics" -type "componentList" 1 "vtx[11:12]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 127.40734650595698 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweak -n "polyTweak2";
	rename -uid "7D38AB31-C249-44CD-D103-F99B38343483";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[11:12]" -type "float3"  0 -35.35534286 -14.64465714
		 0 0 0;
createNode polyMergeVert -n "polyMergeVert5";
	rename -uid "984C819C-194A-813A-9988-3C8651B30AB2";
	setAttr ".ics" -type "componentList" 1 "vtx[4:5]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 184.50456869964302 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweakUV -n "polyTweakUV3";
	rename -uid "0F9B39C4-FE4C-37F9-7066-EA848DB81574";
	setAttr ".uopa" yes;
	setAttr -s 4 ".uvtk";
	setAttr ".uvtk[11]" -type "float2" -7.0348718e-09 1.2353671e-08 ;
	setAttr ".uvtk[13]" -type "float2" -7.1703568e-08 0.15123665 ;
	setAttr ".uvtk[15]" -type "float2" -7.3341653e-09 9.8623838e-09 ;
createNode polyMergeVert -n "polyMergeVert6";
	rename -uid "4D63433C-FB4E-E3AD-7DAA-5B9BD64BAB9B";
	setAttr ".ics" -type "componentList" 1 "vtx[9:10]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 184.50456869964302 0 0 1;
	setAttr ".d" 1e-06;
createNode polyTweak -n "polyTweak3";
	rename -uid "5CF4D7FE-5D44-5EE9-EED4-74A9CCF28885";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[9:10]" -type "float3"  0 -50.000007629395 -50 0 0
		 -4.4888469e-16;
createNode polySoftEdge -n "polySoftEdge1";
	rename -uid "F1372224-A14F-943A-21DC-B4B3DFF30A05";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[13]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 63.045361123929744 0 0 1;
	setAttr ".a" 0;
createNode polySoftEdge -n "polySoftEdge2";
	rename -uid "59F30CF6-3049-5616-F484-58995CC956D1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[33]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 127.40734650595698 0 0 1;
	setAttr ".a" 0;
createNode polySoftEdge -n "polySoftEdge3";
	rename -uid "C4660B27-914D-0712-7522-869E7949811D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[23]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 184.50456869964302 0 0 1;
	setAttr ".a" 0;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 6 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 8 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
select -ne :defaultRenderingList1;
select -ne :defaultTextureList1;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polySoftEdge1.out" "SliceShape1.i";
connectAttr "polyTweakUV1.uvtk[0]" "SliceShape1.uvst[0].uvtw";
connectAttr "polySoftEdge2.out" "SliceShape3.i";
connectAttr "polyTweakUV2.uvtk[0]" "SliceShape3.uvst[0].uvtw";
connectAttr "polySoftEdge3.out" "SliceShape2.i";
connectAttr "polyTweakUV3.uvtk[0]" "SliceShape2.uvst[0].uvtw";
connectAttr "lambert2.oc" "MechanicalLauncherBaseSG.ss";
connectAttr "MechanicalLauncherBaseSG.msg" "materialInfo1.sg";
connectAttr "lambert2.msg" "materialInfo1.m";
connectAttr "lambert3.oc" "LauncherBarSG.ss";
connectAttr "LauncherBarSG.msg" "materialInfo2.sg";
connectAttr "lambert3.msg" "materialInfo2.m";
relationship "link" ":lightLinker1" "MechanicalLauncherBaseSG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "LauncherBarSG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert4SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert5SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "MechanicalLauncherBaseSG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "LauncherBarSG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert4SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert5SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "MatLauncher.oc" "lambert4SG.ss";
connectAttr "lambert4SG.msg" "materialInfo3.sg";
connectAttr "MatLauncher.msg" "materialInfo3.m";
connectAttr "file1.oc" "MatSlice.c";
connectAttr "MatSlice.oc" "lambert5SG.ss";
connectAttr "SliceShape4.iog" "lambert5SG.dsm" -na;
connectAttr "SliceShape1.iog" "lambert5SG.dsm" -na;
connectAttr "SliceShape3.iog" "lambert5SG.dsm" -na;
connectAttr "SliceShape2.iog" "lambert5SG.dsm" -na;
connectAttr "lambert5SG.msg" "materialInfo4.sg";
connectAttr "MatSlice.msg" "materialInfo4.m";
connectAttr "file1.msg" "materialInfo4.t" -na;
connectAttr ":defaultColorMgtGlobals.cme" "file1.cme";
connectAttr ":defaultColorMgtGlobals.cfe" "file1.cmcf";
connectAttr ":defaultColorMgtGlobals.cfp" "file1.cmcp";
connectAttr ":defaultColorMgtGlobals.wsn" "file1.ws";
connectAttr "place2dTexture1.c" "file1.c";
connectAttr "place2dTexture1.tf" "file1.tf";
connectAttr "place2dTexture1.rf" "file1.rf";
connectAttr "place2dTexture1.mu" "file1.mu";
connectAttr "place2dTexture1.mv" "file1.mv";
connectAttr "place2dTexture1.s" "file1.s";
connectAttr "place2dTexture1.wu" "file1.wu";
connectAttr "place2dTexture1.wv" "file1.wv";
connectAttr "place2dTexture1.re" "file1.re";
connectAttr "place2dTexture1.of" "file1.of";
connectAttr "place2dTexture1.r" "file1.ro";
connectAttr "place2dTexture1.n" "file1.n";
connectAttr "place2dTexture1.vt1" "file1.vt1";
connectAttr "place2dTexture1.vt2" "file1.vt2";
connectAttr "place2dTexture1.vt3" "file1.vt3";
connectAttr "place2dTexture1.vc1" "file1.vc1";
connectAttr "place2dTexture1.o" "file1.uv";
connectAttr "place2dTexture1.ofs" "file1.fs";
connectAttr "polySurfaceShape1.o" "polyMergeVert1.ip";
connectAttr "SliceShape1.wm" "polyMergeVert1.mp";
connectAttr "polyMergeVert1.out" "polyTweakUV1.ip";
connectAttr "polyTweak1.out" "polyMergeVert2.ip";
connectAttr "SliceShape1.wm" "polyMergeVert2.mp";
connectAttr "polyTweakUV1.out" "polyTweak1.ip";
connectAttr "polySurfaceShape2.o" "polyMergeVert3.ip";
connectAttr "SliceShape3.wm" "polyMergeVert3.mp";
connectAttr "polyMergeVert3.out" "polyTweakUV2.ip";
connectAttr "polyTweak2.out" "polyMergeVert4.ip";
connectAttr "SliceShape3.wm" "polyMergeVert4.mp";
connectAttr "polyTweakUV2.out" "polyTweak2.ip";
connectAttr "polySurfaceShape3.o" "polyMergeVert5.ip";
connectAttr "SliceShape2.wm" "polyMergeVert5.mp";
connectAttr "polyMergeVert5.out" "polyTweakUV3.ip";
connectAttr "polyTweak3.out" "polyMergeVert6.ip";
connectAttr "SliceShape2.wm" "polyMergeVert6.mp";
connectAttr "polyTweakUV3.out" "polyTweak3.ip";
connectAttr "polyMergeVert2.out" "polySoftEdge1.ip";
connectAttr "SliceShape1.wm" "polySoftEdge1.mp";
connectAttr "polyMergeVert4.out" "polySoftEdge2.ip";
connectAttr "SliceShape3.wm" "polySoftEdge2.mp";
connectAttr "polyMergeVert6.out" "polySoftEdge3.ip";
connectAttr "SliceShape2.wm" "polySoftEdge3.mp";
connectAttr "MechanicalLauncherBaseSG.pa" ":renderPartition.st" -na;
connectAttr "LauncherBarSG.pa" ":renderPartition.st" -na;
connectAttr "lambert4SG.pa" ":renderPartition.st" -na;
connectAttr "lambert5SG.pa" ":renderPartition.st" -na;
connectAttr "lambert2.msg" ":defaultShaderList1.s" -na;
connectAttr "lambert3.msg" ":defaultShaderList1.s" -na;
connectAttr "MatLauncher.msg" ":defaultShaderList1.s" -na;
connectAttr "MatSlice.msg" ":defaultShaderList1.s" -na;
connectAttr "place2dTexture1.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "file1.msg" ":defaultTextureList1.tx" -na;
// End of Projectiles.ma
