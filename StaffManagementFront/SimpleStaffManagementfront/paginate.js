let page = {
    totalItems: 0,
    itemsPerPage: 12,
    pages: 0,
}


paginate = (mainStaffList) => {
    if (mainStaffList.length != 0) {
        let data = []
        page.totalItems = mainStaffList.length;
        page.pages = Math.ceil(page.totalItems / page.itemsPerPage);
        var i = 0;
        while (i < page.totalItems) {
            var temp = mainStaffList
            data.push(temp.slice(i, i + page.itemsPerPage))
            i = i + page.itemsPerPage
        }
        if (currentPage >= pagedStaffList.length && pagedStaffList.length > 0) {
            currentPage = pagedStaffList.length - 1;
        }
        return data;
    } else {
        return mainStaffList;
    }
}