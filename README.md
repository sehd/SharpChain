[![BCH compliance](https://bettercodehub.com/edge/badge/sehd/SharpChain?branch=master)](https://bettercodehub.com/)
[![Build status](https://ci.appveyor.com/api/projects/status/7yy25bg3yfc6fsp7?svg=true)](https://ci.appveyor.com/project/sehd/sharpchain)

# SharpChain
A simple block-chain framework written in C#

## Goals

This project should create a framework to do these:

1. Create and manage a block-chain storage for storing of blocks linked to the previous block via a custom hash function
2. Give helper methods to store and retrieve data from block-chain
3. Handle security and signing in message passing and addind items to chain
	1. Give helper methods to create required signing keys
	2. Verify blocks as they are being created using the signing keys
4. Have multiple persisting strategies for easier use

## How to Use

How to use is simple. There is a `ChainFactory` that creates an object of type `Chain`. All you need is here in the chain object.

```C#
var chain = ChainFactory.Create<string>();
```

#### Creating a Block

If you are creatign your own block first get its hash with `CreateBlock` and the add it to chain with `AddBlock`

```C#
var message = "I'm a new block";
var hash = chain.CreateBlock(message);
chain.AddBlock(message, hash);
```

#### Send the Block

You then send the previously created block along with its hash.

#### Receiving and Validating a Block

If you have received a block it has a content and a hash. You then just call the `AddBlock` method of the chain to insert it in your local block-chain. Listen for the Boolean result of this method which checks the incoming block and returns true only if this block matches the chain so far and then adds it to the chain.

```C#
if(chain.AddBlock(message, hash))
{
	//The block is ok
}
else
{
	//The block is corrupted.
}
```

## Example

An example is also available which is just a proof of concept type of project. **ChainSaw** is a sample chat application that allows two client to chat using a server. The server in this case is different with other chat application servers in that it's a dummy message passing proxy and the data is kept and verified in client side using block-chains.