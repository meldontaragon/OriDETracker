#!/bin/bash

## installs all website components into the desired directory

# testing
_install_dir=/home/admin/test

_user=http
_group=http

_ignore_patterns='--exclude=ori/tracker --exclude=ori/testing --exclude=.git --exclude=.gitignore --exclude=*~ --exclude=*.sh'

rsync --chown=$_user:$_group \
      $_ignore_patterns \
      --recursive \
      --links \
      --update \
      --delete \
      --times \
      --itemize-changes \
      ./ $_install_dir/

echo ''
echo 'Done.'
echo ''
echo 'Checking consistency...'
echo ''

diff --recursive \
     --no-dereference \
     -d \
     $_ignore_patterns \
     . $_install_dir
