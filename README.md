A solver for the game Boggle. It can handle boards up to 3 dimensions. 

'q' and 'u' are treated as separate characters in this solver unlike the real game. It also can't handle words with snowmen (U+2603) in them since this character is used internally by the Trie implementation to determine if a particular path is a word or not.

It uses the 12dicts 2of4brif word list of ~60,000 words obtained from http://wordlist.sourceforge.net/