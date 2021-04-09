FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY ./content/words-english.txt /App/content/.
COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
VOLUME [ "words-english.txt" ] App/content/
ENTRYPOINT ["dotnet", "BluePrism.WordLadder.dll"]
ENV COMPlus_EnableDiagnostics=0
CMD [ "mike", "host", "App/content/words-english.txt", "./answer.txt" ]
