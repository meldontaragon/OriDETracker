#!/bin/bash

## installs all website components into the desired directory

# testing
_install_dir=/home/admin/test

_user=http
_group=http
nn
_ignore_patterns='--exclude=ori/tracker --exclude=ori/testing --exclude=.git --exclude=.gitignore --exclude=*.sh --exclude-from=.gitignore'

install_dir () {
    local cur_dir=$1

    [ ! -d $_install_dir/$cur_dir ] && mkdir -vp $_install_dir/$cur_dir
    [ -d $_install_dir/$cur_dir ] && chown -v $_user:$_group $_install_dir/$cur_dir

    for item in $cur_dir/*; do
	if [[ "$item" =~ "./update-webpage.sh" ]]; then
	    continue;
	else
	    if [[ ( -d $item ) && ( ! -h $item ) ]]; then
		install_dir $item
	    elif [[ ( -h $item ) ]]; then
		rsync -li $item $_install_dir/$item
		chown -v $_user:$_group $_install_dir/$item
	    elif [[ ( -f $item ) ]]; then
		install -v -D --group=$_group --owner=$_user --mode=640 -T $item $_install_dir/$item
	    fi
	fi
    done
}
   
install_dir .

echo ''
echo 'Done.'
echo ''
echo 'Checking consistency...'
echo ''

diff --recursive --no-dereference -d $_ignore_patterns . $_install_dir
