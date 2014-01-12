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
            Trie currentNode = this;

            foreach (var c in wordToFind)
            {
                if (currentNode._node.ContainsKey(c))
                {
                    currentNode = currentNode._node[c];
                }
                else
                {
                    return false;
                }
            }

            return currentNode.IsTerminal;
        }

        public bool ContainsPrefix(string prefixToFind)
        {
            Trie currentNode = this;

            foreach (var c in prefixToFind)
            {
                if (currentNode._node.ContainsKey(c))
                {
                    currentNode = currentNode._node[c];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return PrintTrie(this);
        }

        private string PrintTrie(Trie root)
        {
            if (root == null)
            {
                return TerminalChar.ToString();
            }

            var sb = new StringBuilder();

            foreach (char c in root._node.Keys)
            {
                sb.Append("{" + c + " : " + PrintTrie(root._node[c]) + "}");
            }

            return sb.ToString();
        }
    }
}