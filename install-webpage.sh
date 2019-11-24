#!/bin/bash

## installs all website components into the desired directory

_install_dir=$1

_user=http
_group=http

install_dir () {
    local cur_dir=$1

    [ ! -d $_install_dir/$cur_dir ] && mkdir -p $_install_dir/$cur_dir
    [ -d $_install_dir/$cur_dir ] && chown $_user:$_group $_install_dir/$cur_dir

    for item in $cur_dir/*; do
	if [[ "$item" =~ "*.sh" ]]; then
	    continue;
	else
	    if [[ ( -d $item ) && ( ! -h $item ) ]]; then
		install_dir $item
	    elif [[ ( -h $item ) ]]; then
		cp -P $item $_install_dir/$item
		chown $_user:$_group $_install_dir/$item
	    elif [[ ( -f $item ) ]]; then
		install -Dm640 --group=$_group --owner=$_user -T $item $_install_dir/$item
	    fi
	fi
    done
}

cd ./webroot
install_dir .
