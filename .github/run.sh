#!/bin/bash

set -e

while IFS='' read -r -d '' filename; do
	dir=${filename%/*}
	dir="${dir##*/}"
	found=0
	# This is to prevent grep from prematurely terminating the script.
	grep -q "$dir" Exercism.slnx || found=$?
	if (( found > 0 )); then
		red=$(tput -Txterm-256color setaf 1)
		default=$(tput -Txterm-256color sgr0)
		printf "%bProject '%s' is not included in the solution%b\n" "$red" "$dir" "$default"
		exit 1
	fi
done < <(find . -type f -name "*.csproj" -maxdepth 2 -print0)

no_test=0
no_lint=0

while (( $# > 0 )); do
   case "$1" in
   	--help)
			printf "run.sh [OPTION]... [DIR]\n"
			printf "options:\n"
			printf "\t--help			Show help\n"
			printf "\t--no-test		Skip tests\n"
			printf "\t--no-lint		Skip linting\n"
			exit 0
      	;;
      --no-test)
			no_test=1
			shift
      	;;
      --no-lint)
			no_lint=1
			shift
			;;
		*)
			break
	      ;;
   esac
done

basedir="${1:-.}"

if (( no_test == 0 )); then
  dotnet test --logger "console;verbosity=detailed" --no-restore "$basedir"
fi

if (( no_lint == 0 )); then
	if [[ -z "${CI}" ]]; then
    dotnet format -v n --no-restore "$basedir"
  else
    dotnet format -v n --no-restore --verify-no-changes "$basedir"
  fi
fi
