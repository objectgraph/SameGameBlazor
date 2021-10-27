# SameGame (さめがめ)

SameGame implementation in .net blazor server.

To learn blazor server I wanted to try something i created in multiple other languages. 

## How to play

- Select cells that are the same color (have to be two or more)
- Once selected, you will see how much is the selection worth
- Click on any item that is already selected to remove the cells and accumulate the score.

## Demo
You can play the current deployed code here.

[http://samegame.objectgraph.com/](http://samegame.objectgraph.com/)

## This implmentation

There are [multiple](https://en.wikipedia.org/wiki/SameGame) implementations of SameGame. This implementation is unique as it allows for unlimited undo and redo and the main objective is to score high on a given board with the secondary objective of removing all items.


## Scoring

`n` is the number of cells removed.

This implementation uses `n*(n-1) * 10` scoring. Although it could be changed to suite any other scoring systems easily.

### Various other scoring ways

- `(n-1)^2`
- `(n-2)^2`
- `n^2 -3n + 4` 
