export class ValidateMap {

  private map: number[][] = [];

  constructor() {
  }

  validate(map: number[][]): void {
    this.map = map;
    const width = this.map[0].length;
    const height = this.map.length;

    this.checkEdges(width, height);
    this.checkRoomsSize(width, height);
  }

  private checkEdges(width: number, height: number): void {
    for (let i = 0; i < height; i++) {
      for (let j = 0; j < width; j++) {
        if (
          (i === 0 || i === height - 1 || j === 0 || j === width - 1) &&
          this.map[i][j] !== 1 &&
          this.map[i][j] !== 3
        ) {
          throw new Error(`Invalid edge tile at (${i}, ${j}): ${this.map[i][j]}`);
        }
      }
    }
  }

  private checkRoomsSize(width: number, height: number): void {
    const mapCopy = this.map.map(row => [...row]);

    for (let i = 0; i < height - 1; i++) {
      for (let j = 0; j < width - 1; j++) {
        if (mapCopy[i][j] === 1) {
          if (mapCopy[i + 1][j] === 1 &&
            mapCopy[i][j + 1] === 1 &&
            mapCopy[i + 1][j + 1] === 1) {

            mapCopy[i][j] = 0;
            mapCopy[i + 1][j] = 0;
            mapCopy[i][j + 1] = 0;
            mapCopy[i + 1][j + 1] = 0;

            if(!this.checkIfHasAccessToCorridor(i,j)) {
              throw new Error(`No acess to corridor  at (${i}, ${j})`);
            }
          } else {
            throw new Error(`Invalid room structure at (${i}, ${j})`);
          }
        }
      }
    }
  }

  private checkIfHasAccessToCorridor(i: number, j: number): boolean {

    const isAdjacentToCorridor = (x: number, y: number): boolean => {
      if (x < 0 || y < 0 || x >= this.map.length || y >= this.map[0].length) {
        return false;
      }
      return this.map[x][y] === 2 || this.map[x][y] === 3;
    };

    const cellsToCheck = [
      [i, j],
      [i + 1, j],
      [i, j + 1],
      [i + 1, j + 1]
    ];

    for (const [x, y] of cellsToCheck) {
      if (
        isAdjacentToCorridor(x - 1, y) ||
        isAdjacentToCorridor(x + 1, y) ||
        isAdjacentToCorridor(x, y - 1) ||
        isAdjacentToCorridor(x, y + 1)
      ) {
        return true;
      }
    }
    return false;
  }
}
