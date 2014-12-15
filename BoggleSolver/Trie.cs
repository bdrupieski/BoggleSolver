using System.Collections.Generic;
using System.Text;

namespace BoggleSolver
{
    /// <summary>
    /// This trie implementation will fail on words with snowmen (☃) in them.
    /// </summary>
    public class Trie
    {
        private readonly Dictionary<char, Trie> _node;

        private const char TerminalChar = '☃';

        private bool IsTerminal { get { return _node.ContainsKey(TerminalChar); } }

        private Trie()
        {
            _node = new Dictionary<char, Trie>();
        }

        public static Trie BuildTrie(IEnumerable<string> words)
        {
            var root = new Trie();

            foreach (var word in words)
            {
                Trie currentNode = root;

                foreach (char c in word)
                {
                    if (!currentNode._node.ContainsKey(c))
                    {
                        currentNode._node[c] = new Trie();
                    }

                    currentNode = currentNode._node[c];

                }

                currentNode._node[TerminalChar] = null;
            }

            return root;
        }

        public bool ContainsWord(string wordToFind)
        {
            Trie lastNode = FindLastNodeIfExists(wordToFind);
            return lastNode != null && lastNode.IsTerminal;
        }

        public bool ContainsPrefix(string prefixToFind)
        {
            return FindLastNodeIfExists(prefixToFind) != null;
        }

        private Trie FindLastNodeIfExists(string word)
        {
            Trie currentNode = this;

            foreach (var c in word)
            {
                if (!currentNode._node.TryGetValue(c, out currentNode))
                {
                    return null;
                }
            }

            return currentNode;
        }

        public override string ToString()
        {
            if (_node == null)
            {
                return TerminalChar.ToString();
            }

            var sb = new StringBuilder();

            foreach (char c in _node.Keys)
            {
                sb.Append("{" + c + " : " + _node[c] + "}");
            }

            return sb.ToString();
        }
    }
}