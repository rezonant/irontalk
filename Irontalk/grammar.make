#!/bin/bash

if [ "Irontalk.grammar" -nt "Parser/IrontalkParser.cs" ]; then
	echo "Rebuilding parser from grammar..."
	java -jar grammatica-1.5.jar Irontalk.grammar --csoutput Parser --cspublic
else
	echo "Parser is up to date."
fi
