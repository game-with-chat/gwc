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
			config.allowUnfree = true;
		};
	in
	{
		devShell.${system} = pkgs.mkShell {
			packages = with pkgs; [
				unityhub
				mono
				
			];
		};
	};
}
