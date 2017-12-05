class Day2 {

    static void main(String[] args) {
        Day2 app = new Day2()
        File file = new File("src/main/resources/Day2.txt")
        println(app.sChecksum(file))
    }

    List<Integer> array(String s) {
        s.trim().split('\t').collect{it as int}
    }

    int difference(List<Integer> list) {
        list.max() - list.min()
    }

    int sChecksum(File file) {
        int cksum = 0
        for (String line : file)
            cksum += difference(array(line))
        cksum
    }
}
