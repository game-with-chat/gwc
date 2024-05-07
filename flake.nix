{
	inputs = {
		nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
		#nixpkgs-monodevelop.url = "github:nixos/nixpkgs/e722007bf05802573b41701c49da6c8814878171";
	};

	outputs = {
	nixpkgs,
	...}:
	let
		system = "x86_64-linux";
		pkgs = import nixpkgs {
			inherit system;
			config = {
				allowUnfree = true;
				android_sdk.accept_license = true;
			};
		};
		unityFhs = pkgs.buildFHSEnv {
			name = "gwc-fhs";
			targetPkgs = pkgs: with pkgs; [
				unityhub
				mono
				dotnet-sdk
				vscode
				epiphany
				krita
				feh
				 git
				gitRepo
				gnupg
				curl
				procps
				openssl
				gnumake
				nettools
				# For nixos < 19.03, use `androidenv.platformTools`
				androidenv.androidPkgs_9_0.platform-tools
				jdk
				schedtool
				util-linux
				m4
				gperf
				perl
				libxml2
				zip
				unzip
				bison
				flex
				lzop
				python3
			];
  profile = ''
    export ALLOW_NINJA_ENV=true
    export USE_CCACHE=1
    export ANDROID_JAVA_HOME=${pkgs.jdk.home}sdkmanager install avd
    export LD_LIBRARY_PATH=/usr/lib:/usr/lib32
  '';
  multiPkgs = pkgs: with pkgs;
    [ zlib
      ncurses5
    ];
		};
	in
	{
		# devShell.${system} = pkgs.mkShell {
		# 	packages = with pkgs; [
		# 		unityhub
		# 		mono
		# 		dotnet-sdk
		# 	];
		# 	shellHook = ''
		# 		unityhub
		# 	'';
		# };

		defaultPackage.${system} = unityFhs;
	};
}
