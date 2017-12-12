class Day5 {

    static String PATH = "src/main/resources/Day5.txt"

    static void main(String[] args) {
        Day5 app = new Day5()
        File file = new File(PATH)

        List<Integer> list = file.readLines().collect { it.trim() as int }

        println("${app.stepCount(list)}")
        println("${app.advancedCount(list)}")
    }

    int stepCount(List<Integer> list) {
        int steps = 0
        int position = 0

        while (position < list.size()) {
            int instruction = list[position]

            if (instruction == 0) {
                list[position] += 1
            } else {
                list[position] += 1
                position += instruction
            }
            steps++
        }
        return steps
    }

    int advancedCount(List<Integer> list) {
        int steps = 0
        int position = 0

        while (position < list.size()) {
            steps++
            int instruction = list[position]
            list[position] += 1
            if(instruction >= 3){
                list[position] -= 2
            }
            position += instruction
        }
        return steps
    }
}
