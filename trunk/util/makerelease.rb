#!/usr/bin/ruby

$package = nil
$version = nil
$checked = Array.new(0)
$file = Array.new(0)

def read_manifest(prefix, path)
	#puts "processing #{path}"
	$checked << path
	File.open(path) do |manifest|
		manifest.each do |line|
			if /#.*/ =~ line
				#nothing
			elsif /PACKAGE (.*)/ =~ line
				$package = $1
			elsif /VERSION (.*)/ =~ line
				$version = $1
			elsif /INCLUDE (.*)/ =~ line
				dir, _ = File::split($1)
				read_manifest(dir + '/' , $1)
			else
				if prefix == nil
					$file << line
				else
					$file << prefix + line
				end
			end
		end
	end
end

$*.each do |arg|
	read_manifest(nil,arg)
end
filelist = '' 
$file.each do |f|
	f = f.sub(/\s+/,'')
	filelist += f + ' '
end
command = "zip #{$package}-#{$version}.zip #{filelist}"
`#{command}`

