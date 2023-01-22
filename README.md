<h1 align="center">TCP</h1>
A simple TCPServer and TCPClient

# Features
```cs
You can create a local TCPServer to communicate via the TCPClient with it
```

# How the code works

## TCPClient
This program creates a TCP client that connects to a TcpServer at a specific IP address and port. Once connected, the client sends a message "Hello World!" to the server, then receives a response and prints it to the console. The program uses the TcpClient, NetworkStream, and Encoding classes from the System.Net and System.Text namespaces to handle the communication between the client and server. If there are any errors such as ArgumentNullException or SocketException, it will be caught and printed to the console. 

## TCPServer
The Program listens for incoming client connections on port 13000 of the local host (IP address 127.0.0.1). Once a client connects, the server starts a new thread to handle communication with the client. The server reads data sent by the client, converts it to uppercase, and sends it back to the client as a response. The server continues to listen for incoming connections and handle them in separate threads until it is terminated.

## Error/Fixes
> When finding an Error or a way to improve this program feel free to contact me: NotFabi#3973
