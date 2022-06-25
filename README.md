# Realtime Stock Data Using Signalr
This project showcases the realtime data service from [Tallac Options](wwww.tallacoptions.com). It creates a basic dashboard to fetch live stock quotes.


![Stock Dashboard Screenshot](Capture.PNG)
# Specifications
- **Project Type**: WPF App
- **Target Framework**: .NET 6.0
- **Dependencies**: Signalr Client Core

# Limitations
- TallacOptions-API doesn't cover all [instruments](https://www.tallacoptions.com/instruments). Before you add a new ticker, make sure it's covered.
- The realtime data service operates on trading days and hours.
- The connection goes idle after a while. Authentication and authorization are required for persistant connections.
