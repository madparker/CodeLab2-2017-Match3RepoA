using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLMatchManager : MatchManagerScript {

    public static GLMatchManager instance;

    private List<Vector2> destroyPositions = new List<Vector2>();

    private void Awake() {
        instance = this;
    }

    public override bool GridHasMatch() {
        bool _isMatch = false;
        int _xGrid = gameManager.gridWidth - 2;
        int _yGrid = gameManager.gridHeight - 2;

        for (int i = 0; i < gameManager.gridWidth; i++) {
            for (int j = 0; j < gameManager.gridHeight; j++) {
                if ((i < _xGrid && GridHasHorizontalMatch(i, j)) || (j < _yGrid && GridHasVerticalMatch(i, j))) {
                    _isMatch = true;
                }
            }
        }

        return _isMatch;
    }
    public bool GridHasVerticalMatch(int _x, int _y) {
        GameObject _token0 = gameManager.gridArray[_x, _y + 0];
        GameObject _token1 = gameManager.gridArray[_x, _y + 1];
        GameObject _token2 = gameManager.gridArray[_x, _y + 2];

        if (_token0 != null && _token1 != null && _token2 != null) {
            SpriteRenderer _sprite0 = _token0.GetComponent<SpriteRenderer>();
            SpriteRenderer _sprite1 = _token1.GetComponent<SpriteRenderer>();
            SpriteRenderer _sprite2 = _token2.GetComponent<SpriteRenderer>();
            return (_sprite0.sprite == _sprite1.sprite && _sprite1.sprite == _sprite2.sprite);
        } else {
            return false;
        }
    }

    public int GetVerticalMatchLength(int _x, int _y) {
        int _matchLength = 1;
        GameObject _first = gameManager.gridArray[_x, _y];

        if (_first != null) {
            SpriteRenderer _sprite0 = _first.GetComponent<SpriteRenderer>();

            for (int i = _y + 1; i < gameManager.gridHeight; i++) {
                GameObject _other = gameManager.gridArray[_x, i];
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
        int _horizontalMatchCheck = gameManager.gridWidth - 2;
        int _verticalMatchCheck = gameManager.gridHeight - 2;
        for (int x = 0; x < gameManager.gridWidth; x++) {
            for (int y = 0; y < gameManager.gridHeight; y++) {

                if (x < _horizontalMatchCheck) {
                    int _horizonMatchLength = GetHorizontalMatchLength(x, y);
                    if (_horizonMatchLength > 2) {
                        for (int i = x; i < x + _horizonMatchLength; i++) {
                            destroyPositions.Add(new Vector2(i, y));
                        }
                    }
                }
                if (y < _verticalMatchCheck) {
                    int _verticalMatchLength = GetVerticalMatchLength(x, y);
                    if (_verticalMatchLength > 2) {
                        for (int i = y; i < y + _verticalMatchLength; i++) {
                            destroyPositions.Add(new Vector2(x, i));
                        }
                    }
                }
            }
        }


        foreach (Vector2 t_removePos in destroyPositions) {
            GameObject _token = gameManager.gridArray[(int)t_removePos.x, (int)t_removePos.y];

            if (!_token) {
                continue;
            }
            Destroy(_token);
            gameManager.gridArray[(int)t_removePos.x, (int)t_removePos.y] = null;
            _numRemoved++;
        }
        destroyPositions.Clear();
        return _numRemoved;
    }
}