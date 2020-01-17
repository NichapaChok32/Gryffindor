FROM gcr.io/google-appengine/aspnetcore:2.1
COPY . /app
WORKDIR /app
RUN apt-get -y install lsof
ENTRYPOINT ["dotnet", "Gryffindor.dll"]
