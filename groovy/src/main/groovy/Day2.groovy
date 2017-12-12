class Day2 {

    static void main(String[] args) {
        Day2 app = new Day2()
        File file = new File("src/main/resources/Day2.txt")
        println(app.sChecksum(file))
        println(app.sChecksumDivison(file))
    }

    List<Integer> array(String s) {
        s.trim().split('\t').collect { it as int }
    }

    int difference(List<Integer> list) {
        list.max() - list.min()
    }

    int division(List<Integer> list) {
        int d = 0
        int i = 0
        while (d == 0 && i < list.size() - 1) {
            for (int j = i + 1; j < list.size(); j++) {
                if (list[i] % list[j] == 0 && list[i] != list[j]) {
                    d = (list[i] / list[j]) as int
                    break
                }
            }
            i++
        }
        return d
    }

    int sChecksum(File file) {
        int cksum = 0
        for (String line : file)
            cksum += difference(array(line))
        cksum
    }

    int sChecksumDivison(File file) {
        int cksum = 0
        for (String line : file)
            cksum += division(array(line).sort { -it })
        cksum
    }

}
