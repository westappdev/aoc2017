class Day3 {


    static void main(String[] args) {
        Day3 app = new Day3()
        println("${app.distance(265149)}")
    }

    int distance(int number) {
        if (number == 1)
            return 0

        int steps = 0
        int key = 1
        int bottomRight = 1
        while (bottomRight < number) {
            key += 2
            bottomRight = key**2
            steps += 2
        }

        if (number == bottomRight) {
            return steps
        } else {
            return steps - (bottomRight - number) % (Math.round(steps / 2))
        }


    }
}
