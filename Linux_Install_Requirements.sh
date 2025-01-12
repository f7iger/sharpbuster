#!/bin/bash

function packages(){
	sudo apt install mono-complete mono-mcs mono-runtime mono-utils
}

function build(){
	if [[ -z `dpkg -l | grep -o mono-complete` ]]
	then
		echo -e "Build error: \n Package 'mono-complete' not found or not installed"
	else
		mcs -r:System.Net.Http.dll -out:bin/sharpbuster SharpBuster.cs get.cs
		echo "Builded"
		echo "binary is in './bin' directorie"
	fi
	return 0
}

function install(){
	cp ./bin/sharpbuster /usr/bin/
	echo "sharpbuster installed successfuly"
	echo "Availabe in a new session"
	return 0
}

if [[ "$USER" != 'root' ]]
then
	echo "ERROR: run as root"
	exit 1
fi

packages
build
install
