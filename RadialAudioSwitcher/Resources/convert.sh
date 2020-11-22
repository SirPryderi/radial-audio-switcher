#!/bin/bash

magick convert \
			"PNG/icon@1x.png" "PNG/icon@1.5x.png" "PNG/icon@2x.png" "PNG/icon@4x.png" \
			-colors 256 ICO/icon.ico