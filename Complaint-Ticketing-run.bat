set berryFolder=C:\Users\Ahmad\Desktop\Complaint ticketing application
wt -w 0 nt -d "%berryFolder%\Complaint ticketing" --title APIs --suppressApplicationTitle --tabColor #00afbb cmd /k dotnet run --no-build
wt -w 0 nt -d "%berryFolder%\WebApp\ComplaintTicketingSPA" --title NG --suppressApplicationTitle --tabColor #000000 cmd /k npm run watch