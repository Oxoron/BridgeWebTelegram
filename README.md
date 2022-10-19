# BridgeWebTelegram
A simple web service to receive messages via Web and send them to the end user into Telegram. 
A nice solution to notify owner of X that something is wrong with this X, hiding an info about the owner.

# How to setup locally?
Register your Telegram bot with a BotFather.

Copy appsettings.Development.json to appsettings.Prod.json.
Add 2 props:
	"OwnerTelegramId": 0,
    "botToken": "token"
	
Enjoy!

# Local tests
A simple curl request may be used for testing. Try 
	curl --header "Content-Type: application/json" -d "{\"resource\":\"Hello Telegram\"}" https://localhost:44389/notification/post
You should get a message from your Telegram bot in a second.