using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMatchManager : MatchManagerScript {

    private List<Vector2> destoryList = new List<Vector2>();

    public override bool GridHasMatch() {
        int _xGrid = gameManager.gridWidth - 2;
        int _yGrid = gameManager.gridHeight - 2;

        for (int x = 0; x < gameManager.gridWidth; x++) {
            for (int y = 0; y < gameManager.gridHeight; y++) {
                if ((x < _xGrid && GridHasHorizontalMatch(x, y)) || (y < _yGrid && GridHasVerticalMatch(x, y))) {
                    return true;
                }
            }
        }

        return false;
    }
    public bool GridHasVerticalMatch(int x, int y) {
        GameObject _token0 = gameManager.gridArray[x, y + 0];
        GameObject _token1 = gameManager.gridArray[x, y + 1];
        GameObject _token2 = gameManager.gridArray[x, y + 2];

        if (_token0 != null && _token1 != null && _token2 != null) {
            SpriteRenderer _sprite0 = _token0.GetComponent<SpriteRenderer>();
            SpriteRenderer _sprite1 = _token1.GetComponent<SpriteRenderer>();
            SpriteRenderer _sprite2 = _token2.GetComponent<SpriteRenderer>();
            return (_sprite0.sprite == _sprite1.sprite && _sprite1.sprite == _sprite2.sprite);
        } else {
            return false;
        }
    }

    public int GetVerticalMatchLength(int x, int y) {
        int _matchLength = 1;
        GameObject _first = gameManager.gridArray[x, y];

        if (_first != null) {
            SpriteRenderer _sprite0 = _first.GetComponent<SpriteRenderer>();

            for (int i = y + 1; i < gameManager.gridHeight; i++) {
                GameObject _other = gameManager.gridArray[x, i];
                if (_other != null) {
                    SpriteRenderer _sprite1 = _other.GetComponent<SpriteRenderer>();
                    if (_sprite0.sprite == _sprite1.sprite) {
                        _matchLength++;
                    } else {
                        break;
                    }
                } else {
                    break;
                }
            }
        }
        return _matchLength;
    }

    public override int RemoveMatches() {
        int _numRemoved = 0;
        int horizontalMatchCheck = gameManager.gridWidth - 2;
        int verticalMatchCheck = gameManager.gridHeight - 2;
        for (int x = 0; x < gameManager.gridWidth; x++) {
            for (int y = 0; y < gameManager.gridHeight; y++) {

                if (x < horizontalMatchCheck) {
                    int horizonMatchLength = GetHorizontalMatchLength(x, y);
                    if (horizonMatchLength > 2) {
                        for (int i = x; i < x + horizonMatchLength; i++) {
                            destoryList.Add(new Vector2(i, y));
                        }
                    }
                }
                if (y < verticalMatchCheck) {
                    int verticalMatchLength = GetVerticalMatchLength(x, y);
                    if (verticalMatchLength > 2) {
                        for (int i = y; i < y + verticalMatchLength; i++) {
                            destoryList.Add(new Vector2(x, i));
                        }
                    }
                }
            }
        }


        foreach (Vector2 t_removePos in destoryList) {
            GameObject _token = gameManager.gridArray[(int)t_removePos.x, (int)t_removePos.y];

            if (!_token) {
                continue;
            }
            Destroy(_token);
            gameManager.gridArray[(int)t_removePos.x, (int)t_removePos.y] = null;
            _numRemoved++;
        }
        destoryList.Clear();
        return _numRemoved;
    }
}

