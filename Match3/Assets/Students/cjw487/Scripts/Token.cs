using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chrs
{
    public enum TokenType
    {
        CIRCLE,
        TRIANGLE,
        DIAMOND,
        PENTAGON,
        HEXAGON
    }

    public class Token : MonoBehaviour
    {
        public TokenType type { get; private set; }

        private SpriteRenderer _spriteRenderer;
        // Use this for initialization
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            switch(gameObject.name)
            {
                case "Circle":
                    type = TokenType.CIRCLE;
                    break;
                case "Triangle":
                        type = TokenType.TRIANGLE;
                    break;
                case "Diamond":
                    type = TokenType.DIAMOND;
                    break;
                case "Pentagon":
                    type = TokenType.PENTAGON;
                    break;
                case "Hexagon":
                    type = TokenType.HEXAGON;
                    break;
                default:
                    break;
            }
        }

        public void SetToken(int tokenIndex)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            switch (tokenIndex)
            {
                case 0:
                    _spriteRenderer.sprite = Services.Prefabs.Token[0].GetComponent<SpriteRenderer>().sprite;
                    type = TokenType.CIRCLE;
                    break;
                case 1:
                    _spriteRenderer.sprite = Services.Prefabs.Token[1].GetComponent<SpriteRenderer>().sprite;
                    type = TokenType.TRIANGLE;
                    break;
                case 2:
                    _spriteRenderer.sprite = Services.Prefabs.Token[2].GetComponent<SpriteRenderer>().sprite;
                    type = TokenType.DIAMOND;
                    break;
                case 3:
                    _spriteRenderer.sprite = Services.Prefabs.Token[3].GetComponent<SpriteRenderer>().sprite;
                    type = TokenType.PENTAGON;
                    break;
                case 4:
                    _spriteRenderer.sprite = Services.Prefabs.Token[4].GetComponent<SpriteRenderer>().sprite;
                    type = TokenType.HEXAGON;
                    break;
                default:
                    break;

            }
        }

        private void ChangeToken() { }

        // Update is called once per frame
        void Update()
        {

        }
    }
}