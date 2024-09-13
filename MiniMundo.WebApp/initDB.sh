#!/bin/bash
set -e

dotnet tool install --global dotnet-ef

dotnet ef database update --project MiniMundo.DAL