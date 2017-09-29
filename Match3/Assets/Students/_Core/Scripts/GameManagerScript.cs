using UnityEngine;
using System.Collections;


public class GameManagerScript : MonoBehaviour {

	public int gridWidth = 8;
	public int gridHeight = 8;
	public float tokenSize = 1;

	protected MatchManagerScript matchManager;
	protected InputManagerScript inputManager;
	protected RepopulateScript repopulateManager;
	protected MoveTokensScript moveTokenManager;

	public GameObject grid;
	public  GameObject[,] gridArray;
	protected Object[] tokenTypes;
	GameObject selected;

	public virtual void Start () {
		tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
        gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();
		matchManager = GetComponent<MatchManagerScript>();
		inputManager = GetComponent<InputManagerScript>();
		repopulateManager = GetComponent<RepopulateScript>();
		moveTokenManager = GetComponent<MoveTokensScript>();
	}

	public virtual void Update()
    {
		if(!GridHasEmpty())
        {
			if(matchManager.GridHasMatch())
            {
				matchManager.RemoveMatches();
			}
            else
            {
				inputManager.SelectToken();
			}
		}
        else
        {
			if(!moveTokenManager.move)
            {
				moveTokenManager.SetupTokenMove();
			}
			if(!moveTokenManager.MoveTokensToFillEmptySpaces())
            {
				repopulateManager.AddNewTokensToRepopulateGrid();
			}
		}
	}

    /// <summary>
    /// Makes grid based on height and width variables
    /// </summary>
	void MakeGrid()
    {
		grid = new GameObject("TokenGrid");
        //  Makes grid based on width and height variables
		for(int x = 0; x < gridWidth; x++)
        {
			for(int y = 0; y < gridHeight; y++)
            {
                //  Fills in the grid with tokens
				AddTokenToPosInGrid(x, y, grid);
			}
		}
	}

    /// <summary>
    /// Checks if there is an empty node in the grid
    /// </summary>
    /// <returns>True if a node of the grid is empty</returns>
	public virtual bool GridHasEmpty()
    {
		for(int x = 0; x < gridWidth; x++)
        {
			for(int y = 0; y < gridHeight ; y++)
            {
				if(gridArray[x, y] == null)
                {
					return true;
				}
			}
		}

		return false;
	}

    /// <summary>
    /// Gets the position of the token in the grid
    /// </summary>
    /// <param name="token">The token we want to find the position of</param>
    /// <returns>Token's position in the grid in a Vector 2</returns>
	public Vector2 GetPositionOfTokenInGrid(GameObject token)
    {
		for(int x = 0; x < gridWidth; x++)
        {
			for(int y = 0; y < gridHeight ; y++)
            {
				if(gridArray[x, y] == token)
                {
					return(new Vector2(x, y));
				}
			}
		}

        // If position not found, return an empty Vector 2
		return new Vector2();
	}
		
    /// <summary>
    /// Gets grid position and translate the position into Unity world space
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <returns> A Vector2 with the grid position width and height in world space</returns>
	public Vector2 GetWorldPositionFromGridPosition(int x, int y)
    {
		return new Vector2(
			(x - gridWidth/2) * tokenSize,
			(y - gridHeight/2) * tokenSize);
	}

    /// <summary>
    /// Places tokens at positions x, y of the parent grid.
    /// </summary>
    /// <param name="x">X position</param>
    /// <param name="y">Y position</param>
    /// <param name="parent"> Parent grid</param>
	public void AddTokenToPosInGrid(int x, int y, GameObject parent)
    {
		Vector3 position = GetWorldPositionFromGridPosition(x, y);

        //  Gets random token from the tokenType array
        GameObject token = 
			Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], 
			            position, 
			            Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		gridArray[x, y] = token;
	}
}
